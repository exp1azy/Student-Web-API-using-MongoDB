using MongoDB.Driver;

namespace StudentAPI
{
    public class MongoDb
    {
        public static IMongoCollection<T> GetCollection<T>(string name, IMongoDatabase database)
        {
            return database.GetCollection<T>(name);
        }
    }
}
