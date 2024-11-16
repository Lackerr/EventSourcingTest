using EventSourcingTest.Events;
using EventSourcingTest.Models;
using Newtonsoft.Json;

namespace EventSourcingTest;

public class DocumentRehydrator
{
    public Document Rehydrate(Guid documentId, List<StoredEvent> storedEvents)
    {
        var document = new Document();

        foreach (var storedEvent in storedEvents)
        {
            switch (storedEvent.Type)
            {
                case nameof(DocumentCreated):
                    var documentCreated = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentCreated>(storedEvent.Data);
                    document.Apply(documentCreated);
                    break;

                case nameof(VersionAdded):
                    var versionAdded = Newtonsoft.Json.JsonConvert.DeserializeObject<VersionAdded>(storedEvent.Data);
                    document.Apply(versionAdded);
                    break;

                default:
                    throw new Exception($"Unbekannter Event-Typ: {storedEvent.Type}");
            }
        }

        return document;
    }
}
