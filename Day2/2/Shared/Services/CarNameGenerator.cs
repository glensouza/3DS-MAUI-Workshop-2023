using Shared.Interfaces;
using Shared.Models;
using Shared.Utilities;
using HtmlAgilityPack;

namespace Shared.Services;

public class CarNameGenerator : IRandomPictureGenerator
{
    private readonly HttpClient httpClient;
    private readonly HtmlDocument htmlDoc;
    private readonly List<string> carNames;
    private const string CarDoesNotExistUrl = "https://www.thisautomobiledoesnotexist.com";

    public CarNameGenerator()
    {
        this.httpClient = new HttpClient();
        this.htmlDoc = new HtmlDocument();
        this.carNames = Constants.CarNames;
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
