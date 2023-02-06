using MongoDB.Driver;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class ExamService
    {
        private readonly IMongoDatabase _database;

        public ExamService(IMongoDatabase database) 
        { 
            _database = database;
        }

        public async Task<Exam> GetExamByStringIdAsync(string id)
        {
            var examCollection = MongoDb.GetCollection<Exam>("Exam", _database);
            return await examCollection.Find(i => i._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Exam>> GetExamsByDisciplineNameAsync(string disciplineName)
        {
            var examCollection = MongoDb.GetCollection<Exam>("Exam", _database);
            return await examCollection.Find(i => i.Subject == disciplineName).ToListAsync();
        }

        public async Task AddExamAsync(Exam exam)
        {
            var examCollection = MongoDb.GetCollection<Exam>("Exam", _database);
            await examCollection.InsertOneAsync(exam);
        }

        public async Task UpdateExamAsync(Exam exam)
        {
            var examCollection = MongoDb.GetCollection<Exam>("Exam", _database);
            await examCollection.ReplaceOneAsync(i => i._id == exam._id, exam);
        }

        public async Task DeleteExamAsync(string id)
        {
            var examCollection = MongoDb.GetCollection<Exam>("Exam", _database);
            await examCollection.DeleteOneAsync(i => i._id == id);
        }
    }
}
