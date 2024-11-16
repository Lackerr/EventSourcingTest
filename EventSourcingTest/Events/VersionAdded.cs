namespace EventSourcingTest.Events;

public class VersionAdded
{
    public Guid DocumentId { get; set; }
    public int VersionNumber { get; set; }
    public string FilePath { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime TimeStamp { get; set; }
}