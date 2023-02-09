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

        [HttpGet("passed-before-date/{date}")]
        public async Task<IActionResult> GetExamsPassedBeforeSpecifiedDate(DateTime date)
        {
            return Ok(await _examService.GetExamsPassedBeforeSpecifiedDateAsync(date));
        }

        [HttpGet("mark-below-good")]
        public async Task<IActionResult> GetExamsWithMarkBelowGood()
        {
            return Ok(await _examService.GetExamsWithMarkBelowGoodAsync());
        }

        [HttpGet("mark-higher-good")]
        public async Task<IActionResult> GetExamsWithMarkHigherGood()
        {
            return Ok(await _examService.GetExamsWithMarkHigherGoodAsync());
        }

        [HttpGet("get-string/{id}")]
        public async Task<IActionResult> GetExamById(string id)
        {
            var existingExam = await _examService.GetExamByIdAsync(id);

            if (existingExam is null)
            {
                return NotFound();
            }

            return Ok(existingExam);
        }

        [HttpGet("get-by-discipline/{discipline}")]
        public async Task<IActionResult> GetExamsByDisciplineName(string discipline)
        {
            var existingExam = await _examService.GetExamsByDisciplineNameAsync(discipline);

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
            var existingExam = await _examService.GetExamByIdAsync(id);

            if (existingExam is null)
                return BadRequest();

            exam._id = existingExam._id;
            await _examService.UpdateExamAsync(exam);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var existingExam = await _examService.GetExamByIdAsync(id);

            if (existingExam is null)
                return BadRequest();

            await _examService.DeleteExamAsync(id);

            return NoContent();
        }
    }
}
