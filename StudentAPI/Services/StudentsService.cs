using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentAPI.Models;
using System.ComponentModel;

namespace StudentAPI.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Students> _students;

        public StudentsService(IOptions<StudentsDBSettings> dbSettings) 
        { 
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _students = mongoDb.GetCollection<Students>(dbSettings.Value.StudentsCollection);
        }

        public async Task<List<Students>> GetAllAsync()
        {
            return await _students.Find(i => true).ToListAsync();
        }

        public async Task<Students> GetByIdAsync(int id)
        {
            return await _students.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Students> GetByStringIdAsync(string id)
        {
            return await _students.Find(i => i._id == id).FirstOrDefaultAsync();
        }

        public async Task AddStudentAsync(Students student)
        {
            await _students.InsertOneAsync(student);
        }

        public async Task UpdateStudentAsync(Students student)
        {
            await _students.ReplaceOneAsync(i => i.Id == student.Id, student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _students.DeleteOneAsync(i => i.Id == id);
        }
    }
}
