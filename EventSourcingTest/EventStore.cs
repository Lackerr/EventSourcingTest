using EventSourcingTest.Models;
using Newtonsoft.Json;

namespace EventSourcingTest;

public class EventStore
{
    private readonly List<StoredEvent> _events = new();

    public void Save<T>(T domainEvent)
    {
        var storedEvent = new StoredEvent
        {
            Type = domainEvent.GetType().Name,
            Data = JsonConvert.SerializeObject(domainEvent)
        };
        _events.Add(storedEvent);
    }
    
    public List<StoredEvent> GetAllEvents()
    {
        return _events;
    }
}