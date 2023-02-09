using MongoDB.Driver;
using StudentAPI.InterimModels;
using StudentAPI.Models;
using System.Text.RegularExpressions;

namespace StudentAPI.Services
{
    public class StudGroupService
    {
        private readonly GeneralRepository _generalRepository;
        private readonly StudGroupRepository _studGroupRepository;

        public StudGroupService(StudGroupRepository studGroupRepository, GeneralRepository generalRepository)
        {
            _studGroupRepository = studGroupRepository;
            _generalRepository = generalRepository;
        }

        public async Task<List<GroupCountModel>> GetGroupsWhereMoreMenAsync()
        {
            var groupCollection = await _generalRepository.GetAllAsync<StudGroup>("StudGroup");
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");

            var groupCounts = new List<GroupCountModel>();

            foreach (var group in groupCollection)
            {
                int menCount = studCollection.Count(i => i.GroupNum == group.GroupNum && i.Gender == "М");
                int womenCount = studCollection.Count(i => i.GroupNum == group.GroupNum && i.Gender == "Ж");

                if (menCount > womenCount)
                {
                    groupCounts.Add(new GroupCountModel
                    {
                        GroupNum = group.GroupNum,
                        MenCount = menCount,
                        WomenCount = womenCount
                    });
                }              
            }
            
            return groupCounts;
        }

        public async Task<List<GroupNumModel>> GetGroupsWithBadMarksAsync()
        {
            var groupCollection = await _generalRepository.GetAllAsync<StudGroup>("StudGroup");
            var examCollection = await _generalRepository.GetAllAsync<Exam>("Exam");
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");

            var badGroups = groupCollection.Join(studCollection, g => g.GroupNum, s => s.GroupNum,
                (g, s) => new { GroupNum = g.GroupNum, Course = g.Course, Dep = g.Dep, StudId = s.Id })
                .Join(examCollection, i => i.StudId, e => e.StudId,
                (i, e) => new { StudId = i.StudId, GroupNum = i.GroupNum, Course = i.Course, Dep = i.Dep, Mark = e.Mark })
                .Where(i => i.Mark == 25).ToList();

            var result = new List<GroupNumModel>();

            foreach (var group in badGroups)
            {
                result.Add(new GroupNumModel
                {
                    StudId = group.StudId,
                    GroupNum = group.GroupNum,
                    Course = group.Course,
                    Dep = group.Dep
                });
            }

            return result.DistinctBy(i => i.GroupNum).ToList();
        }

        public async Task<List<GroupNumModel>> GetGroupWithSpecifiedStudentAsync(int id)
        {
            var groupCollection = await _generalRepository.GetAllAsync<StudGroup>("StudGroup");
            var studCollection = await _generalRepository.GetAllAsync<Students>("Students");

            var result = groupCollection.Join(studCollection, g => g.GroupNum, s => s.GroupNum,
                (g, s) => new GroupNumModel { GroupNum = g.GroupNum, Course = g.Course, Dep = g.Dep, StudId = s.Id })
                .Where(i => i.StudId == id).ToList();

            return result;
        }

        public async Task<StudGroup> GetGroupByStringIdAsync(string id)
        {
            return await _studGroupRepository.GetGroupByStringIdAsync(id);
        }

        public async Task<List<StudGroup>> GetGroupsByCourseOrDirectionAsync(int course, string? direction)
        {
            return await _studGroupRepository.GetGroupsByCourseOrDirectionAsync(course, direction);
        }

        public async Task AddStudGroupAsync(StudGroup group)
        {
            await _studGroupRepository.UpdateStudGroupAsync(group);
        }

        public async Task UpdateStudGroupAsync(StudGroup group)
        {
            await _studGroupRepository.UpdateStudGroupAsync(group);
        }

        public async Task DeleteStudGroupAsync(string id)
        {
            await _studGroupRepository.DeleteStudGroupAsync(id);
        }
    }
}
