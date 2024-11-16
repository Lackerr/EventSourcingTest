using EventSourcingTest.Events;

namespace EventSourcingTest.Models;

public class Document
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<DocumentVersion> Versions { get; set; } = new();

    public void Apply(DocumentCreated domainEvent)
    {
        Id = domainEvent.DocumentId;
        Title = domainEvent.Title;
        CreatedBy = domainEvent.CreatedBy;
        CreatedAt = domainEvent.TimeStamp;
    }

    public void Apply(VersionAdded domainEvent)
    {
        var version = new DocumentVersion
        {
            VersionNumber = domainEvent.VersionNumber,
            FilePath = domainEvent.FilePath,
            ModifiedBy = domainEvent.ModifiedBy,
            ModifiedAt = domainEvent.TimeStamp
        };
        Versions.Add(version);
    }
}
