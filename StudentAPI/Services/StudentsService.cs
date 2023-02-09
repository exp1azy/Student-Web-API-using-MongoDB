using StudentAPI.InterimModels;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class StudentsService
    {
        private readonly GeneralRepository _generalRepository;
        private readonly StudentsRepository _studentsRepository;

        public StudentsService(GeneralRepository generalRepository, StudentsRepository studentsRepository)
        {
            _generalRepository = generalRepository;
            _studentsRepository = studentsRepository;
        }

        public async Task<List<StudExamModel>> GetStudentsWithMarkAboveSpecifiedAsync(int mark)
        {
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");

            var result = studCollection.Join(examCollection, s => s.Id, e => e.StudId,
                (s, e) => new StudExamModel{ Id = s.Id, Name = s.Name, Exam = e.Subject, Mark = e.Mark })
                .Where(i => i.Mark > mark).ToList();

            return result;
        }

        public async Task<List<StudLectModel>> GetStudentsWhosePassedBySpecifiedLecturerAsync(string lecturerName)
        {
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");
            var lectCollection = await _generalRepository.GetAllAsync<Lecturers>("Lecturers");

            var result = studCollection.Join(examCollection, s => s.Id, e => e.StudId, 
                (s, e) => new { LectId = e.LectId, StudId = s.Id, StudName = s.Name, Exam = e.Subject })
                .Join(lectCollection, i => i.LectId, l => l.Id, 
                (i, l) => new StudLectModel{ Id = i.StudId, StudName = i.StudName, Exam = i.Exam, LectName = l.Name })
                .Where(i => i.LectName.Equals(lecturerName)).ToList();

            return result;
        }

        public async Task<List<StudGroupNumModel>> GetStudentsFromSpecifiedGroupAsync(string groupNum)
        {
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");
            var groupCollection = await _generalRepository.GetAllAsync<StudGroup>("StudGroup");

            var result = studCollection.Join(groupCollection, s => s.GroupNum, g => g.GroupNum,
                (s, g) => new StudGroupNumModel{ StudName = s.Name, Course = g.Course, Dep = g.Dep, GroupNum = g.GroupNum})
                .Where(i => i.GroupNum.Equals(groupNum)).ToList();

            return result;
        }

        public async Task<Students> GetStudentByIdAsync(int id)
        {
            return await _studentsRepository.GetStudentByIdAsync(id);
        }

        public async Task<Students> GetStudentByIdAsync(string id)
        {
            return await _studentsRepository.GetStudentByIdAsync(id);
        }

        public async Task AddStudentAsync(Students student)
        {
            await _studentsRepository.AddStudentAsync(student);
        }

        public async Task UpdateStudentAsync(Students student)
        {
            await _studentsRepository.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentsRepository.DeleteStudentAsync(id);
        }
    }
}
