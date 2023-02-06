using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : Controller
    {
        private readonly ExamService _examService;

        public ExamController(ExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetExamById(string id)
        {
            var existingExam = await _examService.GetExamByStringIdAsync(id);

            if (existingExam is null)
            {
                return NotFound();
            }

            return Ok(existingExam);
        }

        [HttpGet("get-exams/{disciplineName}")]
        public async Task<IActionResult> GetExamsByDisciplineName(string disciplineName)
        {
            var existingExam = await _examService.GetExamsByDisciplineNameAsync(disciplineName);

            if (existingExam is null) 
            { 
                return NotFound();
            }

            return Ok(existingExam);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddExam(Exam exam)
        {
            await _examService.AddExamAsync(exam);

            return CreatedAtAction(nameof(GetExamById), new { id = exam._id }, exam);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateExam(string id, Exam exam)
        {
            var existingExam = await _examService.GetExamByStringIdAsync(id);

            if (existingExam is null)
            {
                return BadRequest();
            }

            exam._id = existingExam._id;
            await _examService.UpdateExamAsync(exam);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var existingExam = await _examService.GetExamsByDisciplineNameAsync(id);

            if (existingExam is null)
            {
                return BadRequest();
            }

            await _examService.DeleteExamAsync(id);

            return NoContent();
        }
    }
}
