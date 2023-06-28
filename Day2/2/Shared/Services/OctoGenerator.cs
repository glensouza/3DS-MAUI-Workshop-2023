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
