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
