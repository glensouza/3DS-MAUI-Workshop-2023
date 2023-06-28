# Shared Project

Delete the default "`Class1.cs`" file from the Shared project.

Let's set up the folders to organize the shared classes needed to be created.

- [Models](#models)
- [Interfaces](#interfaces)
- [Data](#data)
- [Services](#services)
- [Utilities](#utilities)

## NuGet packages

### Azure.Data.Tables

We're going to use Azure Table Storage as our data store.  We'll need a class to represent the data we'll be storing in the table.

1. Right-click on the **Shared** project
1. Select **Manage NuGet Packages...**
1. Select the **Browse** tab
1. Search for **Azure.Data.Tables**
1. Select the **Azure.Data.Tables** package
1. Click the **Install** button
1. Click the **OK** button

### Azure.Storage.Blobs

We will use this package to upload images to Azure Blob Storage.

1. Right-click on the **Shared** project
1. Select **Manage NuGet Packages...**
1. Select the **Browse** tab
1. Search for **Azure.Storage.Blobs**
1. Select the **Azure.Storage.Blobs** package
1. Click the **Install** button
1. Click the **OK** button

### HtmlAgilityPack

We will use this package to parse the HTML from the [This Automobile Does Not Exist](https://www.thisautomobiledoesnotexist.com) website.

1. Right-click on the **Shared** project
1. Select **Manage NuGet Packages...**
1. Select the **Browse** tab
1. Search for **HtmlAgilityPack**
1. Select the **HtmlAgilityPack** package
1. Click the **Install** button
1. Click the **OK** button

### System.ServiceModel.Syndication

We will use this package to read the RSS feed from the [GitHub Octodex](https://octodex.github.com) website.

1. Right-click on the **Shared** project
1. Select **Manage NuGet Packages...**
1. Select the **Browse** tab
1. Search for **System.ServiceModel.Syndication**
1. Select the **System.ServiceModel.Syndication** package
1. Click the **Install** button
1. Click the **OK** button

## Models

1. Right-click on the **Shared** project
1. Select **Add > New Folder**
1. Name the folder **Models**
1. Right-click on the **Models** folder
1. Select **Add > Class**
1. Name the class **Constants.cs**
1. Replace the file content with this:

    ```csharp
    namespace Shared.Models;

    public static class Constants
    {
        public const string VoteTableName = "votes";
        public const string PictureTableName = "pictures";
        public const string CarTablePartitionKey = "Car";
        public const string OctoTablePartitionKey = "Octo";
    }
    ```

1. Right-click on the **Models** folder
1. Select **Add > Class**
1. Name the class **PictureEntity.cs**
1. Replace the file content with this:

    ```csharp
    using Azure;

    namespace Shared.Models;

    public class PictureEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = string.Empty;
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string PictureUri { get; set; } = string.Empty;
        public string PictureSmlUri { get; set; } = string.Empty;
        public double Rating { get; set; } = 1200;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
    ```

1. Right-click on the **Models** folder
1. Select **Add > Class**
1. Name the class **VoteEntity.cs**
1. Replace the file content with this:

    ```csharp
    using Azure.Data.Tables;
    using Azure;

    public class VoteEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = string.Empty; // Picture
        public string RowKey { get; set; } = string.Empty; // Competitor
        public bool? Won { get; set; } = null;
        public double? Score { get; set; } = null;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
    ```

## Interfaces

1. Right-click on the **Shared** project
1. Select **Add > New Folder**
1. Name the folder **Interfaces**
1. Right-click on the **Interfaces** folder
1. Select **Add > Class**
1. Name the class **IData.cs**
1. Replace the file content with this:

    ```csharp
    namespace Shared.Interfaces;

    public interface IData<T>
    {
        public List<T> GetAll();
        public T? GetById(string id);
        public List<T> GetByName(string name);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(string id);
    }
    ```

1. Right-click on the **Interfaces** folder
1. Select **Add > Class**
1. Name the class **IStorage.cs**
1. Replace the file content with this:

    ```csharp
    namespace Shared.Interfaces;

    public interface IStorage
    {
        public Task<string> Save(MemoryStream stream, string pngFileName);
        public Task<string> Save(byte[] bytes, string pngFileName);
        public Task<string> Save(string base64PNG, string pngFileName);
        public Task<MemoryStream?> Retrieve(string pngFileName);
    }
    ```

1. Right-click on the **Interfaces** folder
1. Select **Add > Class**
1. Name the class **IRandomPictureGenerator.cs**
1. Replace the file content with this:

    ```csharp
    using Shared.Models;

    namespace Shared.Interfaces;

    public interface IRandomPictureGenerator
    {
        public Task<RandomPicture> GetRandomPictureAsync();
    }
    ```

## Data

1. Right-click on the **Shared** project
1. Select **Add > New Folder**
1. Name the folder **Data**
1. Right-click on the **Data** folder
1. Select **Add > Class**
1. Name the class **PictureTable.cs**
1. Replace the contents of the file with this:

    ```csharp
    using Azure;
    using Azure.Data.Tables;
    using Shared.Interfaces;
    using Shared.Models;

    namespace Shared.Data;

    public class PictureTable : IData<PictureEntity>
    {
        private readonly TableClient client;
        private readonly string partitionKey;

        public PictureTable(string storageConnectionString, string partitionKey)
        {
            this.partitionKey = partitionKey;
            this.client = new TableClient(storageConnectionString, Constants.PictureTableName);
            this.client.CreateIfNotExists();
        }

        public List<PictureEntity> GetAll()
        {
            Pageable<PictureEntity> queryPictureTables = this.client.Query<PictureEntity>(s => s.PartitionKey == this.partitionKey);
            List<PictureEntity> pictureTables = queryPictureTables.AsPages().SelectMany(page => page.Values).ToList();
            return pictureTables;
        }

        public PictureEntity? GetById(string id)
        {
            Pageable<PictureEntity> queryPictureToUpdateEntities = this.client.Query<PictureEntity>(s => s.PartitionKey == this.partitionKey && s.RowKey == id);
            PictureEntity? pictureToUpdate = queryPictureToUpdateEntities.AsPages().SelectMany(page => page.Values).FirstOrDefault();
            return pictureToUpdate;
        }

        public List<PictureEntity> GetByName(string name)
        {
            List<PictureEntity> returnPictureEntities = new();
            Pageable<PictureEntity> queryPictureEntities = this.client.Query<PictureEntity>(s => s.PartitionKey == this.partitionKey && s.Name == name);
            PictureEntity? existingPictureEntity = queryPictureEntities.AsPages().SelectMany(page => page.Values).FirstOrDefault();
            if (existingPictureEntity != null)
            {
                returnPictureEntities.Add(existingPictureEntity);
            }

            return returnPictureEntities;
        }

        public async Task AddAsync(PictureEntity entity)
        {
            entity.PartitionKey = this.partitionKey;
            await this.client.AddEntityAsync(entity);
        }

        public async Task UpdateAsync(PictureEntity entity)
        {
            entity.PartitionKey = this.partitionKey;
            await this.client.UpdateEntityAsync(entity, ETag.All, TableUpdateMode.Replace);
        }

        public async Task DeleteAsync(string id)
        {
            await this.client.DeleteEntityAsync(this.partitionKey, id);
        }
    }
    ```

1. Right-click on the **Data** folder
1. Select **Add > Class**
1. Name the class **VoteTable.cs**
1. Replace the contents of the file with this:

    ```csharp
    using Azure;
    using Azure.Data.Tables;
    using Shared.Interfaces;
    using Shared.Models;

    namespace Shared.Data;

    public class VoteTable : IData<VoteEntity>
    {
        private readonly TableClient client;
        private readonly string partitionKey;

        public VoteTable(string storageConnectionString, string partitionKey)
        {
            this.partitionKey = partitionKey;
            this.client = new TableClient(storageConnectionString, Constants.VoteTableName);
            this.client.CreateIfNotExists();
        }

        public List<VoteEntity> GetAll()
        {
            Pageable<VoteEntity> queryVoteTables = this.client.Query<VoteEntity>(s => s.PartitionKey == this.partitionKey);
            List<VoteEntity> voteTables = queryVoteTables.AsPages().SelectMany(page => page.Values).ToList();
            return voteTables;
        }

        public VoteEntity? GetById(string id)
        {
            Pageable<VoteEntity> queryVoteToUpdateEntities = this.client.Query<VoteEntity>(s => s.PartitionKey == this.partitionKey && s.RowKey == id);
            VoteEntity? voteToUpdate = queryVoteToUpdateEntities.AsPages().SelectMany(page => page.Values).FirstOrDefault();
            return voteToUpdate;
        }

        public List<VoteEntity> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(VoteEntity entity)
        {
            entity.PartitionKey = this.partitionKey;
            await this.client.AddEntityAsync(entity);
        }

        public async Task UpdateAsync(VoteEntity entity)
        {
            entity.PartitionKey = this.partitionKey;
            await this.client.UpdateEntityAsync(entity, ETag.All, TableUpdateMode.Replace);
        }

        public async Task DeleteAsync(string id)
        {
            await this.client.DeleteEntityAsync(this.partitionKey, id);
        }
    }
    ```

1. Right-click on the **Data** folder
1. Select **Add > Class**
1. Name the class **BlobStorage.cs**
1. Replace the contents of the file with this:

```csharp
using Azure.Storage.Blobs;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Data;

public class BlobStorage : IStorage
{
    private readonly BlobContainerClient blobContainerClient;

    public BlobStorage(string storageConnectionString)
    {
        BlobContainerClient container = new(storageConnectionString, Constants.BlobStorageContainerName);
        container.CreateIfNotExists();
        this.blobContainerClient = container;
    }

    public async Task<string> Save(MemoryStream stream, string pngFileName)
    {
        BlobClient? pictureCloudBlockBlob = this.blobContainerClient.GetBlobClient($"{pngFileName}.png");
        bool fileExists = await pictureCloudBlockBlob.ExistsAsync();
        while (fileExists)
        {
            await pictureCloudBlockBlob.DeleteAsync();
            fileExists = await pictureCloudBlockBlob.ExistsAsync();
        }

        await pictureCloudBlockBlob.UploadAsync(stream);
        string picUri = pictureCloudBlockBlob.Uri.AbsoluteUri;
        return picUri;
    }

    public async Task<string> Save(byte[] bytes, string pngFileName)
    {
        using MemoryStream stream = new(bytes, 0, bytes.Length);
        return await this.Save(stream, pngFileName);
    }

    public async Task<string> Save(string base64PNG, string pngFileName)
    {
        byte[] bytes = Convert.FromBase64String(base64PNG);
        return await this.Save(bytes, pngFileName);
    }

    public async Task<MemoryStream?> Retrieve(string pngFileName)
    {
        BlobClient? pictureCloudBlockBlob = this.blobContainerClient.GetBlobClient($"{pngFileName}.png");
        bool fileExists = await pictureCloudBlockBlob.ExistsAsync();
        if(!fileExists)
        {
            return null;
        }

        using MemoryStream stream = new();
        await pictureCloudBlockBlob.DownloadToAsync(stream);
        return stream;
    }
}
```

## Services

1. Right-click on the **Shared** project
1. Select **Add > New Folder**
1. Name the folder **Services**

### Car name generator

1. Right-click on the **Services** folder
1. Select **Add > Class**
1. Name the class **CarNameGenerator.cs**
1. Replace the contents of the file with this:

    ```csharp
    using Shared.Interfaces;
    using Shared.Models;
    using Shared.Utilities;
    using HtmlAgilityPack;

    namespace Shared.Services;

    public class CarNameGenerator : IRandomPictureGenerator
    {
        private readonly HttpClient httpClient;
        private readonly HtmlDocument htmlDoc;
        private readonly List<string> carNames = new();
        private const string CarDoesNotExistUrl = "https://www.thisautomobiledoesnotexist.com";

        public CarNameGenerator()
        {
            this.httpClient = new HttpClient();
            this.htmlDoc = new HtmlDocument();
            IEnumerable<string> lines = File.ReadLines("carNames.txt");
            foreach (string line in lines)
            {
                this.carNames.Add(line);
            }
        }

        public async Task<RandomPicture> GetRandomPictureAsync()
        {
            RandomPicture randomPicture = new();

            this.carNames.Shuffle();
            int firstName = Random.Shared.Next(this.carNames.Count);
            if (this.carNames[firstName].Contains(' '))
            {
                randomPicture.Name = this.carNames[firstName];
            }
            else
            {
                int secondName = Random.Shared.Next(this.carNames.Count);
                if (this.carNames[secondName].Contains(' '))
                {
                    randomPicture.Name = this.carNames[secondName];
                }
                else
                {
                    string generatedCarName = $"{this.carNames[firstName]} {this.carNames[secondName]}";
                    randomPicture.Name = generatedCarName;
                }
            }

            string carDoesNotExistHtml = await this.httpClient.GetStringAsync(CarDoesNotExistUrl);
            if (string.IsNullOrEmpty(carDoesNotExistHtml))
            {
                throw new Exception("website down!");
            }

            this.htmlDoc.LoadHtml(carDoesNotExistHtml);

            // Check if the image exists
            HtmlNode imgNode = this.htmlDoc.DocumentNode.SelectSingleNode("//img[@id='vehicle']") ?? throw new Exception("website down!");

            string src = imgNode.GetAttributeValue("src", string.Empty);
            randomPicture.Base64PNG = src.Replace("data:image/png;base64,", string.Empty);

            return randomPicture;
        }
    }
    ```

### Octo generator

1. Right-click on the **Services** folder
1. Select **Add > Class**
1. Name the class **OctoGenerator.cs**
1. Replace the contents of the file with this:

    ```csharp
    using System.ServiceModel.Syndication;
    using System.Xml;
    using Shared.Interfaces;
    using Shared.Models;
    using Shared.Utilities;

    namespace Shared.Services;

    public class OctoGenerator : IRandomPictureGenerator
    {
        private const string URL = "https://octodex.github.com/atom.xml";

        public async Task<RandomPicture> GetRandomPictureAsync()
        {
            RandomPicture randomPicture = new();

            using XmlReader reader = XmlReader.Create(URL);
            SyndicationFeed? feed = SyndicationFeed.Load(reader);

            using HttpClient client = new();

            List<SyndicationItem> feeds = feed.Items.ToList();
            feeds.Shuffle();
            foreach (SyndicationItem syndicationItem in feeds)
            {
                string title = syndicationItem.Title.Text;
                string content = ((TextSyndicationContent)syndicationItem.Content).Text;

                string imageSrc = content[(content.IndexOf("src=\"", StringComparison.Ordinal) + 5)..];
                string imageUrl = imageSrc[..imageSrc.IndexOf("\"", StringComparison.Ordinal)];
                string filename = imageUrl[(imageUrl.LastIndexOf("/", StringComparison.Ordinal) + 1)..];
                string fileExtension = Path.GetExtension(filename);

                if (fileExtension != ".png")
                {
                    continue;
                }

                using HttpResponseMessage result = await client.GetAsync(imageUrl);
                if (!result.IsSuccessStatusCode)
                {
                    continue;
                }

                byte[] bytes = await result.Content.ReadAsByteArrayAsync();
                string base64 = Convert.ToBase64String(bytes);
                randomPicture.Name = title;
                randomPicture.Base64PNG = base64;
                break;
            }

            return randomPicture;
        }
    }
    ```

### Elo calculator

1. Right-click on the **Services** folder
1. Select **Add > Class**
1. Name the class **EloCalculator.cs**
1. Replace the contents of the file with this:

    ```csharp
    namespace Shared.Services;

    public class EloCalculator
    {
        private const int KFactor = 32;

        public static (double, double) CalculateElo(double winnerRating, double loserRating)
        {
            // Calculate the expected scores for each picture
            double winnerExpectedScore = 1 / (1 + Math.Pow(10, (loserRating - winnerRating) / 400));
            double loserExpectedScore = 1 / (1 + Math.Pow(10, (winnerRating - loserRating) / 400));

            // Update the ratings for each picture
            double winnerScore = KFactor * (1 - winnerExpectedScore);
            double loserScore = KFactor * (0 - loserExpectedScore);

            return (winnerScore, loserScore);
        }
    }
    ```

## Utilities

1. Right-click on the **Shared** project
1. Select **Add > New Folder**
1. Name the folder **Utilities**

### Shuffle List Extention

1. Right-click on the **Utilities** folder
1. Select **Add > Class**
1. Name the class **ShuffleListExtension.cs**
1. Replace the contents of the file with this:

    ```csharp
    using System.Runtime.CompilerServices;

    namespace Shared.Utilities;

    public static class ShuffleListExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            int n = list.Count;
            while (n > 1)
            {
                int k = Random.Shared.Next(n--);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
    ```

[Next: Test with test harness](3-TestHarness.md)
