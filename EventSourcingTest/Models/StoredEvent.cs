namespace EventSourcingTest.Models;

public class StoredEvent
{
    public string Type { get; set; }
    public string Data { get; set; }
}