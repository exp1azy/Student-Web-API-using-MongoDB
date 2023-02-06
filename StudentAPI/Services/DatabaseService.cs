using MongoDB.Driver;

namespace StudentAPI.Services
{
    public class DatabaseService
    {
        private readonly IMongoDatabase _database;

        public DatabaseService(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<List<T>> GetAllAsync<T>(string collectionName)
        {
            var collection = MongoDb.GetCollection<T>(collectionName, _database);
            return await collection.Find(i => true).ToListAsync();
        }
    }
}
