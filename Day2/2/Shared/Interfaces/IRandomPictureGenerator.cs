using Shared.Models;

namespace Shared.Interfaces;

public interface IRandomPictureGenerator
{
    public Task<RandomPicture> GetRandomPictureAsync();
}
