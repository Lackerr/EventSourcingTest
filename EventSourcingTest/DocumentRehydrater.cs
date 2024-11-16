using EventSourcingTest.Events;
using EventSourcingTest.Models;
using EventSourcingTest.Repositories;
using Newtonsoft.Json;

namespace EventSourcingTest;

public class DocumentRehydrator
{
    public async Task<Document> RehydrateAsync(Guid aggregateId, EventStoreRepository repository)
    {
        var events = await repository.GetEventsByAggregateIdAsync(aggregateId);
        var document = new Document();

        foreach (var storedEvent in events)
        {
            var eventType = storedEvent["EventType"].AsString;
            var eventData = storedEvent["EventData"].AsString;

            switch (eventType)
            {
                case nameof(DocumentCreated):
                    var documentCreated = JsonConvert.DeserializeObject<DocumentCreated>(eventData);
                    document.Apply(documentCreated);
                    break;

                case nameof(VersionAdded):
                    var versionAdded = JsonConvert.DeserializeObject<VersionAdded>(eventData);
                    document.Apply(versionAdded);
                    break;

                default:
                    throw new Exception($"Unknown event type: {eventType}");
            }
        }

        return document;
    }
}
