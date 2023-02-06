using MongoDB.Driver;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class LecturersService
    {
        private readonly IMongoDatabase _database;

        public LecturersService(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Lecturers> GetLecturerByIdAsync(int id)
        {
            var lecturersCollection = MongoDb.GetCollection<Lecturers>("Lecturers", _database);
            return await lecturersCollection.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Lecturers> GetLecturerByStringIdAsync(string id)
        {
            var lecturersCollection = MongoDb.GetCollection<Lecturers>("Lecturers", _database);
            return await lecturersCollection.Find(i => i._id == id).FirstOrDefaultAsync();
        }

        public async Task AddLecturerAsync(Lecturers lecturer)
        {
            var collection = MongoDb.GetCollection<Lecturers>("Lecturers", _database);
            await collection.InsertOneAsync(lecturer);
        }

        public async Task UpdateLecturerAsync(Lecturers lecturer)
        {
            var lecturersCollection = MongoDb.GetCollection<Lecturers>("Lecturers", _database);
            await lecturersCollection.ReplaceOneAsync(i => i.Id == lecturer.Id, lecturer);
        }

        public async Task DeleteLecturerAsync(int id)
        {
            var lecturersCollection = MongoDb.GetCollection<Lecturers>("Lecturers", _database);
            await lecturersCollection.DeleteOneAsync(i => i.Id == id);
        }
    }
}
