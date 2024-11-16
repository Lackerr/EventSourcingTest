using Newtonsoft.Json;

namespace EventSourcingTest;

public class EventPublisher
{
    private readonly EventStore _eventStore;

    public EventPublisher(EventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public void Publish<T>(T domainEvent)
    {
        _eventStore.Save(domainEvent);
    }
}