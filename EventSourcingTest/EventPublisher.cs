using EventSourcingTest.Repositories;
using Newtonsoft.Json;

namespace EventSourcingTest;

public class EventPublisher
{
    private readonly EventStoreRepository _repository;

    public EventPublisher(EventStoreRepository repository)
    {
        _repository = repository;
    }

    public async Task PublishAsync<T>(T domainEvent, Guid aggregateId)
    {
        var eventType = domainEvent.GetType().Name;
        var eventData = JsonConvert.SerializeObject(domainEvent);
        await _repository.SaveEventAsync(aggregateId, eventType, eventData);
    }
}