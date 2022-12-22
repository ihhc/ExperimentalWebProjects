using MongoDB.Driver;

namespace DocumentsCore
{
    public interface IDbClient
    {
        IMongoCollection<Document> GetDocumentsCollection();
    }
}
