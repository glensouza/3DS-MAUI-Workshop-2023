namespace Shared.Interfaces;

public interface IStorage
{
    public Task<string> Save(MemoryStream stream, string pngFileName);
    public Task<string> Save(byte[] bytes, string pngFileName);
    public Task<string> Save(string base64PNG, string pngFileName);
    public Task<MemoryStream?> Retrieve(string pngFileName);
}
