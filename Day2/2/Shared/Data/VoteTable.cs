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
