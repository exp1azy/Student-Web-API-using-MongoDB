using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudGroupController : Controller
    {
        private readonly StudGroupService _studGroupService;

        public StudGroupController(StudGroupService studGroupService)
        {
            _studGroupService = studGroupService;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetGroupById(string id)
        {
            var existingGroup = await _studGroupService.GetGroupByStringIdAsync(id);

            if (existingGroup is null)
            {
                return NotFound();
            }

            return Ok(existingGroup);
        }

        [HttpGet("get-by-course-dep")]
        public async Task<IActionResult> GetGroupsByCourseOrDirection(int course, string? direction)
        {
            var existingGroup = await _studGroupService.GetGroupsByCourseOrDirectionAsync(course, direction);

            if (existingGroup is null)
            {
                return NotFound();
            }

            return Ok(existingGroup);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroup(StudGroup studGroup)
        {
            await _studGroupService.AddStudGroupAsync(studGroup);

            return CreatedAtAction(nameof(GetGroupById), new { id = studGroup._id }, studGroup);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateGroup(string id, StudGroup studGroup)
        {
            var existingGroup = await _studGroupService.GetGroupByStringIdAsync(id);

            if (existingGroup is null)
            {
                return BadRequest();
            }

            studGroup._id = existingGroup._id;
            await _studGroupService.UpdateStudGroupAsync(studGroup);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            var existingGroup = await _studGroupService.GetGroupByStringIdAsync(id);

            if (existingGroup is null)
            {
                return BadRequest();
            }

            await _studGroupService.DeleteStudGroupAsync(id);

            return NoContent();
        }
    }
}
