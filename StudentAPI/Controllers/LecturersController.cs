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

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var existingLecturer = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturer is null)
            {
                return NotFound();
            }

            return Ok(existingLecturer);
        }

        [HttpGet("get-string/{id}")]
        public async Task<IActionResult> GetByStringId(string id)
        {
            var existingLecturer = await _lecturersService.GetLecturerByStringIdAsync(id);

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

            return CreatedAtAction(nameof(GetById), new { id = lecturer.Id }, lecturer);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLecturer(int id, Lecturers lecturer)
        {
            var existingLecturer = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturer is null)
            {
                return BadRequest();
            }

            lecturer.Id = existingLecturer.Id;
            await _lecturersService.UpdateLecturerAsync(lecturer);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLecturerAsync(int id)
        {
            var existingLecturer = await _lecturersService.GetLecturerByIdAsync(id);

            if (existingLecturer is null)
            {
                return BadRequest();
            }

            await _lecturersService.DeleteLecturerAsync(id);

            return NoContent();
        }
    }
}
