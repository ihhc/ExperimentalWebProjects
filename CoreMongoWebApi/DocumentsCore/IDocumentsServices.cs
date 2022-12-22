using System.Collections.Generic;

namespace DocumentsCore
{
    public interface IDocumentsServices
    {
        List<Document> GetDocuments();
        Document GetDocument(string id);
        Document AddDocument(Document document);
        void DeleteDocument(string id);
        Document UpdateDocument(Document document);
    }
}
