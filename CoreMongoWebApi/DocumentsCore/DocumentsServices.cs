using MongoDB.Driver;
using System.Collections.Generic;

namespace DocumentsCore
{
    public class DocumentsServices : IDocumentsServices
    {
        private readonly IMongoCollection<Document> _documents;

        public DocumentsServices(IDbClient dbClient)
        {
            _documents = dbClient.GetDocumentsCollection();
        }

        public Document AddDocument(Document document)
        {
            _documents.InsertOne(document);
            return document;
        }

        public void DeleteDocument(string id)
        {
            _documents.DeleteOne(document => document.Id == id);
        }

        public Document GetDocument(string id) => _documents.Find(document => document.Id == id).First();

        public List<Document> GetDocuments() => _documents.Find(documents => true).ToList();

        public Document UpdateDocument(Document document)
        {
            GetDocument(document.Id);
            _documents.ReplaceOne(b => b.Id == document.Id, document);
            return document;
        }
    }
}
