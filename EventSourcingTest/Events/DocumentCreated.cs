namespace EventSourcingTest.Events;

public class DocumentCreated
{
    public Guid DocumentId { get; set; }
    public string Title { get; set; }
    public string CreatedBy { get; set; }
    public DateTime TimeStamp { get; set; }
}