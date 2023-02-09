using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly StudentsService _studentsService;

        public StudentsController(StudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet("mark-above-specified/{mark}")]
        public async Task<IActionResult> GetStudentsWithMarkAboveSpecified(int mark) =>
            Ok(await _studentsService.GetStudentsWithMarkAboveSpecifiedAsync(mark));
        
        [HttpGet("passed-by-specified-lecturer/{lecturerName}")]
        public async Task<IActionResult> GetStudentsWhosePassedBySpecifiedLecturer(string lecturerName) =>    
            Ok(await _studentsService.GetStudentsWhosePassedBySpecifiedLecturerAsync(lecturerName));

        [HttpGet("specified-group/{groupNum}")]
        public async Task<IActionResult> GetStudentsFromSpecifiedGroup(string groupNum) =>
            Ok(await _studentsService.GetStudentsFromSpecifiedGroupAsync(groupNum));

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
            {
                return NotFound();
            }

            return Ok(existingStudent);
        }

        [HttpGet("get-string/{id}")]
        public async Task<IActionResult> GetStudentByStringId(string id)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
            {
                return NotFound();
            }

            return Ok(existingStudent);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudent(Students student)
        {
            await _studentsService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Students student)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
                return BadRequest();

            student.Id = existingStudent.Id;
            await _studentsService.UpdateStudentAsync(student);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
                return BadRequest();

            await _studentsService.DeleteStudentAsync(id);

            return NoContent();
        }
    }
}
