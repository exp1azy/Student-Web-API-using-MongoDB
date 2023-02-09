using MongoDB.Driver;

namespace StudentAPI.Services
{
    public interface IGeneralRepository
    {
        public Task<List<T>> GetAllAsync<T>(string collectionName);
    }

    public class GeneralRepository : IGeneralRepository
    {
        private readonly IMongoDatabase _database;

        public GeneralRepository(IMongoDatabase database)
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
