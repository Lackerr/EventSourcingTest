// See https://aka.ms/new-console-template for more information

using EventSourcingTest;
using EventSourcingTest.Repositories;
using EventSourcingTest.Services;

const string connectionString = "mongodb://localhost:27017";
const string databaseName = "EventSourcingDB";

var repository = new EventStoreRepository(connectionString, databaseName);
var eventPublisher = new EventPublisher(repository);
var documentService = new DocumentService(eventPublisher);
var rehydrator = new DocumentRehydrator();

var documentId = new Guid("00000000-0000-0000-0000-000000000001");

var storedEvents = await repository.GetEventsByAggregateIdAsync(documentId);

if (storedEvents.Count == 0)
{
    Console.WriteLine("Erstelle ein neues Testdokument...");
    await documentService.CreateDocumentAsync(documentId, "Testdokument", "Elias");
}
else
{
    Console.WriteLine("Testdokument gefunden. Zustand wird wiederhergestellt...");
}

var document = await rehydrator.RehydrateAsync(documentId, repository);

var newVersionNumber = document.Versions.Count + 1;
var newFilePath = $"/docs/testdokument_v{newVersionNumber}.pdf";
await documentService.AddVersionAsync(documentId, newVersionNumber, newFilePath, "Elias");

Console.WriteLine($"Neue Version hinzugefügt: Version {newVersionNumber}, Datei: {newFilePath}");

document = await rehydrator.RehydrateAsync(documentId, repository);
Console.WriteLine("Aktueller Zustand des Testdokuments:");
Console.WriteLine($"Dokument ID: {document.Id}");
Console.WriteLine($"Titel: {document.Title}");
Console.WriteLine($"Erstellt von: {document.CreatedBy}");
Console.WriteLine("Versionen:");
foreach (var version in document.Versions)
{
    Console.WriteLine($"  - Version {version.VersionNumber}, Datei: {version.FilePath}");
}

Console.ReadKey();