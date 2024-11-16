// See https://aka.ms/new-console-template for more information

using EventSourcingTest;
using EventSourcingTest.Services;

var eventStore = new EventStore();
var eventPublisher = new EventPublisher(eventStore);
var documentService = new DocumentService(eventPublisher);

var documentId = Guid.NewGuid();
documentService.CreateDocument(documentId, "Projektplan", "Elias");

documentService.AddVersion(documentId, 1, "/docs/projektplan_v1.pdf", "Elias");
documentService.AddVersion(documentId, 2, "/docs/projektplan_v2.pdf", "Elias");

var storedEvents = eventStore.GetAllEvents();

var rehydrator = new DocumentRehydrator();
var document = rehydrator.Rehydrate(documentId, storedEvents);

Console.WriteLine($"Dokument ID: {document.Id}");
Console.WriteLine($"Titel: {document.Title}");
Console.WriteLine($"Erstellt von: {document.CreatedBy}");
Console.WriteLine($"Versionen:");
foreach (var version in document.Versions)
{
    Console.WriteLine($"  - Version {version.VersionNumber}, Datei: {version.FilePath}");
}


Console.ReadKey();