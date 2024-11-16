using MongoDB.Bson;
using MongoDB.Driver;

namespace EventSourcingTest.Repositories;

public class EventStoreRepository
{
    private readonly IMongoCollection<BsonDocument> _eventCollection;

    public EventStoreRepository(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _eventCollection = database.GetCollection<BsonDocument>("EventStore");
    }
    
    public async Task SaveEventAsync(Guid aggregateId, string eventType, string eventData)
    {
        var document = new BsonDocument
        {
            { "AggregateId", aggregateId.ToString() },
            { "EventType", eventType },
            { "EventData", eventData },
            { "CreatedAt", DateTime.UtcNow }
        };

        await _eventCollection.InsertOneAsync(document);
    }

    public async Task<List<BsonDocument>> GetEventsByAggregateIdAsync(Guid aggregateId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("AggregateId", aggregateId.ToString());
        return await _eventCollection.Find(filter).SortBy(e => e["CreatedAt"]).ToListAsync();
    }
}