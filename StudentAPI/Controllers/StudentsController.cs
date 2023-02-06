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

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
            {
                return NotFound();
            }

            return Ok(existingStudent);
        }

        [HttpGet("get-string/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var existingStudent = await _studentsService.GetStudentByStringIdAsync(id);

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

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, Students student)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
            {
                return BadRequest();
            }
            
            student.Id = existingStudent.Id;
            await _studentsService.UpdateStudentAsync(student);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingStudent = await _studentsService.GetStudentByIdAsync(id);

            if (existingStudent is null)
            {
                return BadRequest();
            }

            await _studentsService.DeleteStudentAsync(id);

            return NoContent();
        }
    }
}
