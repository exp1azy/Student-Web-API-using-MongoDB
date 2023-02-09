using MongoDB.Driver;
using StudentAPI.InterimModels;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class LecturersService
    {
        private readonly LecturersRepository _lecturersRepository;
        private readonly GeneralRepository _generalRepository;

        public LecturersService(GeneralRepository generalRepository, LecturersRepository lecturersRepository)
        {
            _generalRepository = generalRepository;
            _lecturersRepository = lecturersRepository;
        }

        public async Task<List<Lecturers>> GetLecturersWithExperienceAboveSpecifiedAsync(int stage)
        {
            var lectCollection = await _generalRepository.GetAllAsync<Lecturers>("Lecturers");
            return lectCollection.Where(i => i.Stage > stage).ToList();
        }

        public async Task<List<LecturersWithSpecifiedStudentsModel>> GetLecturersWhoTookExamFromSpecifiedStudentAsync(string studName)
        {
            var lectCollection = await _generalRepository.GetAllAsync<Lecturers>("Lecturers");
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");

            var result = examCollection.Join(lectCollection, e => e.LectId, l => l.Id,
                (e, l) => new { LectId = l.Id, LectName = l.Name, Exam = e.Subject, StudId = e.StudId })
                .Join(studCollection, i => i.StudId, s => s.Id,
                (i, s) => new LecturersWithSpecifiedStudentsModel { StudName = s.Name, Exam = i.Exam, LectName = i.LectName, LectId = i.LectId })
                .Where(i => i.StudName.Equals(studName)).ToList();

            return result;
        }

        public async Task<List<Lecturers>> GetLecturersFromSpecifiedDepAsync(string dep)
        {
            var lectCollection = await _generalRepository.GetAllAsync<Lecturers>("Lecturers");
            return lectCollection.Where(i => i.Dep.Equals(dep)).ToList();
        }

        public async Task<Lecturers> GetLecturerByIdAsync(int id)
        {
            return await _lecturersRepository.GetLecturerByIdAsync(id);
        }

        public async Task<Lecturers> GetLecturerByIdAsync(string id)
        {
            return await _lecturersRepository.GetLecturerByIdAsync(id);
        }

        public async Task AddLecturerAsync(Lecturers lecturer)
        {
            await _lecturersRepository.AddLecturerAsync(lecturer);
        }

        public async Task UpdateLecturerAsync(Lecturers lecturer)
        {
            await _lecturersRepository.UpdateLecturerAsync(lecturer);
        }

        public async Task DeleteLecturerAsync(int id)
        {
            await _lecturersRepository.DeleteLecturerAsync(id);
        }
    }
}
