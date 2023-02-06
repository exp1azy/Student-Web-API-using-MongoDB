using MongoDB.Driver;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class StudGroupService
    {
        private readonly IMongoDatabase _database;

        public StudGroupService(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<StudGroup> GetGroupByStringIdAsync(string id)
        {
            var studGroupCollection = MongoDb.GetCollection<StudGroup>("StudGroup", _database);
            return await studGroupCollection.Find(i => i._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<StudGroup>> GetGroupsByCourseOrDirectionAsync(int course, string? direction)
        {
            var studGroupCollection = MongoDb.GetCollection<StudGroup>("StudGroup", _database);
            return await studGroupCollection.Find(i => i.Course == course || i.Dep == direction).ToListAsync();
        }

        public async Task AddStudGroupAsync(StudGroup group)
        {
            var studGroupCollection = MongoDb.GetCollection<StudGroup>("StudGroup", _database);
            await studGroupCollection.InsertOneAsync(group);
        }

        public async Task UpdateStudGroupAsync(StudGroup group)
        {
            var studGroupCollection = MongoDb.GetCollection<StudGroup>("StudGroup", _database);
            await studGroupCollection.ReplaceOneAsync(i => i._id == group._id, group);
        }

        public async Task DeleteStudGroupAsync(string id)
        {
            var studGroupCollection = MongoDb.GetCollection<StudGroup>("StudGroup", _database);
            await studGroupCollection.DeleteOneAsync(i => i._id == id);
        }
    }
}
