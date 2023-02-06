using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : Controller
    {
        private readonly DatabaseService _personService;

        public DatabaseController(DatabaseService personService)
        {
            _personService = personService;
        }

        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var allStudents = await _personService.GetAllAsync<Students>("Students");

            if (allStudents.Any())
            {
                return Ok(allStudents);
            }

            return NotFound();
        }

        [HttpGet("get-all-lecturers")]
        public async Task<IActionResult> GetAllLecturers()
        {
            var allLecturers = await _personService.GetAllAsync<Lecturers>("Lecturers");

            if (allLecturers.Any())
            {
                return Ok(allLecturers);
            }

            return NotFound();
        }

        [HttpGet("get-all-exams")]
        public async Task<IActionResult> GetAllExams()
        {
            var allExams = await _personService.GetAllAsync<Exam>("Exam");

            if (allExams.Any())
            {
                return Ok(allExams);
            }

            return NotFound();
        }

        [HttpGet("get-all-stud-groups")]
        public async Task<IActionResult> GetAllStudGroups()
        {
            var allGroups = await _personService.GetAllAsync<StudGroup>("StudGroup");

            if (allGroups.Any())
            {
                return Ok(allGroups);
            }

            return NotFound();
        }
    }
}
