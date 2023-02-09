using MongoDB.Driver;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class ExamService
    {
        private readonly ExamRepository _examRepository;
        private readonly GeneralRepository _generalRepository;

        public ExamService(ExamRepository examRepository, GeneralRepository generalRepository)
        {
            _examRepository = examRepository;
            _generalRepository = generalRepository;
        }

        public async Task<List<Exam>> GetExamsPassedBeforeSpecifiedDateAsync(DateTime date)
        {
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");
            return examCollection.Where(i => i.Date < date).ToList();
        }

        public async Task<List<Exam>> GetExamsWithMarkBelowGoodAsync()
        {
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");
            return examCollection.Where(i => i.Mark < 35).ToList();
        }

        public async Task<List<Exam>> GetExamsWithMarkHigherGoodAsync()
        {
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");
            return examCollection.Where(i => i.Mark >= 35).ToList();
        }

        public async Task<Exam> GetExamByIdAsync(string id)
        {
            return await _examRepository.GetExamByStringIdAsync(id);
        }

        public async Task<List<Exam>> GetExamsByDisciplineNameAsync(string disciplineName)
        {
            return await _examRepository.GetExamsByDisciplineNameAsync(disciplineName);
        }

        public async Task AddExamAsync(Exam exam)
        {
            await _examRepository.AddExamAsync(exam);
        }

        public async Task UpdateExamAsync(Exam exam)
        {
            await _examRepository.UpdateExamAsync(exam);
        }

        public async Task DeleteExamAsync(string id)
        {
            await _examRepository.DeleteExamAsync(id);
        }
    }
}
