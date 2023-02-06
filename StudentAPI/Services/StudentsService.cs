using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentAPI.Models;
using System.ComponentModel;

namespace StudentAPI.Services
{
    public class StudentsService
    {
        private readonly IMongoDatabase _database;      

        public StudentsService(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Students> GetStudentByIdAsync(int id)
        {
            var studentCollection = MongoDb.GetCollection<Students>("Students", _database);
            return await studentCollection.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Students> GetStudentByStringIdAsync(string id)
        {
            var studentCollection = MongoDb.GetCollection<Students>("Students", _database);
            return await studentCollection.Find(i => i._id == id).FirstOrDefaultAsync();
        }

        public async Task AddStudentAsync(Students student)
        {
            var collection = MongoDb.GetCollection<Students>("Students", _database);
            await collection.InsertOneAsync(student);
        }

        public async Task UpdateStudentAsync(Students student)
        {
            var studentCollection = MongoDb.GetCollection<Students>("Students", _database);
            await studentCollection.ReplaceOneAsync(i => i.Id == student.Id, student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var studentCollection = MongoDb.GetCollection<Students>("Students", _database);
            await studentCollection.DeleteOneAsync(i => i.Id == id);
        }
    }
}
