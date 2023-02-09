using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturersController : Controller
    {
        private readonly LecturersService _lecturersService;

        public LecturersController(LecturersService lecturersService)
        {
            _lecturersService = lecturersService;
        }

        [HttpGet("stage-above-specified/{stage}")]
        public async Task<IActionResult> GetLecturersWithExperienceAboveSpecified(int stage)
        {
            return Ok(await _lecturersService.GetLecturersWithExperienceAboveSpecifiedAsync(stage));
        }

        [HttpGet("took-exam-from-spec-student/{studName}")]
        public async Task<IActionResult> GetLecturersWhoTookExamFromSpecifiedStudent(string studName)
        {
            return Ok(await _lecturersService.GetLecturersWhoTookExamFromSpecifiedStudentAsync(studName));
        }

        [HttpGet("from-specified-dep/{dep}")]
        public async Task<IActionResult> GetLecturersFromSpecifiedDep(string dep)
        {
            return Ok(await _lecturersService.GetLecturersFromSpecifiedDepAsync(dep));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetLecturerById(int id)
        {
            var existingLecturers = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturers is null)
            {
                return NotFound();
            }

            return Ok(existingLecturers);
        }

        [HttpGet("get-string/{id}")]
        public async Task<IActionResult> GetLecturerByStringId(string id)
        {
            var existingLecturer = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturer is null)
            {
                return NotFound();
            }

            return Ok(existingLecturer);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddLecturer(Lecturers lecturer)
        {
            await _lecturersService.AddLecturerAsync(lecturer);
            return CreatedAtAction(nameof(GetLecturerById), new { id = lecturer.Id }, lecturer);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLecturer(int id, Lecturers lecturer)
        {
            var existingLecturer = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturer is null)
                return BadRequest();

            lecturer.Id = existingLecturer.Id;
            await _lecturersService.UpdateLecturerAsync(lecturer);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var existingLecturer = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturer is null)
                return BadRequest();

            await _lecturersService.DeleteLecturerAsync(id);

            return NoContent();
        }
    }
}
