using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocumentsCore
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Document> _documents;

        public DbClient(IOptions<TestDbConfig> testDbConfig)
        {
            var client = new MongoClient(testDbConfig.Value.Connection_String);
            var database = client.GetDatabase(testDbConfig.Value.Database_Name);
            _documents = database.GetCollection<Document>(testDbConfig.Value.Test_Collection_Name);
        }

        public IMongoCollection<Document> GetDocumentsCollection() => _documents;
    }
}
