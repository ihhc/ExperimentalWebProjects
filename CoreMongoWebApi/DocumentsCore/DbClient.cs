using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocumentsCore
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Document> _documents;
        private MongoClient _mongoClient = null;
        private IMongoDatabase _mongoDatabase = null;

        public DbClient(IOptions<TestDbConfig> testDbConfig)
        {
            _mongoClient = new MongoClient(testDbConfig.Value.Connection_String);
            _mongoDatabase = _mongoClient.GetDatabase(testDbConfig.Value.Database_Name);
            _documents = _mongoDatabase.GetCollection<Document>(testDbConfig.Value.Test_Collection_Name);
        }

        public IMongoCollection<Document> GetDocumentsCollection() => _documents;
        public IMongoDatabase GetDatabase() => _mongoDatabase;
    }
}
