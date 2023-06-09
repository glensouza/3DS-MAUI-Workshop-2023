﻿using Azure.Data.Tables;
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
