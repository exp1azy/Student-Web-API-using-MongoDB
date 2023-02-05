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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allStudents = await _studentsService.GetAllAsync();

            if (allStudents.Any())
            {
                return Ok(allStudents);
            }

            return NotFound();
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var existingStudent = await _studentsService.GetByIdAsync(id);

            if (existingStudent is null)
            {
                return NotFound();
            }

            return Ok(existingStudent);
        }

        [HttpGet("GetString/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var existingStudent = await _studentsService.GetByStringIdAsync(id);

            if (existingStudent is null)
            {
                return NotFound();
            }

            return Ok(existingStudent);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Students student)
        {
            await _studentsService.AddStudentAsync(student);

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, Students student)
        {
            var existingStudent = await _studentsService.GetByIdAsync(id);

            if (existingStudent is null)
            {
                return BadRequest();
            }
            
            student.Id = existingStudent.Id;
            await _studentsService.UpdateStudentAsync(student);

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingStudent = await _studentsService.GetByIdAsync(id);

            if (existingStudent is null)
            {
                return BadRequest();
            }

            await _studentsService.DeleteStudentAsync(id);

            return NoContent();
        }
    }
}
