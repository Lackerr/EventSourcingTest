﻿using EventSourcingTest.Events;

namespace EventSourcingTest.Services;

public class DocumentService
{
    private readonly EventPublisher _eventPublisher;

    public DocumentService(EventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task CreateDocumentAsync(Guid documentId, string title, string createdBy)
    {
        var domainEvent = new DocumentCreated
        {
            DocumentId = documentId,
            Title = title,
            CreatedBy = createdBy,
            TimeStamp = DateTime.UtcNow
        };
        _eventPublisher.PublishAsync(domainEvent, documentId);
    } 
    
    public async Task AddVersionAsync(Guid documentId, int versionNumber, string filePath, string modifiedBy)
    {
        var domainEvent = new VersionAdded
        {
            DocumentId = documentId,
            VersionNumber = versionNumber,
            FilePath = filePath,
            ModifiedBy = modifiedBy,
            TimeStamp = DateTime.UtcNow
        };
        _eventPublisher.PublishAsync(domainEvent, documentId);
    }
}