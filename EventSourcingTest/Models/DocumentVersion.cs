namespace EventSourcingTest.Models;

public class DocumentVersion
{
    public int VersionNumber { get; set; }
    public string FilePath { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
}
