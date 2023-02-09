using Microsoft.AspNetCore.Mvc;
using StudentAPI.InterimModels;
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

        [HttpGet("get-more-men")]
        public async Task<IActionResult> GetGroupsWhereMoreMen()
        {
            return Ok(await _studGroupService.GetGroupsWhereMoreMenAsync());
        }

        [HttpGet("get-bad-marks")]
        public async Task<IActionResult> GetGroupsWithBadMarks()
        {
            return Ok(await _studGroupService.GetGroupsWithBadMarksAsync());
        }

        [HttpGet("get-student/{id}")]
        public async Task<IActionResult> GetGroupWithSpecifiedStudent(int id)
        {
            return Ok(await _studGroupService.GetGroupWithSpecifiedStudentAsync(id));
        }

        [HttpGet("get-string/{id}")]
        public async Task<IActionResult> GetGroupById(string id)
        {
            var existingGroup = await _studGroupService.GetGroupByStringIdAsync(id);

            if (existingGroup is null)
            {
                return NotFound();
            }

            return Ok(existingGroup);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudGroup(StudGroup studGroup)
        {
            await _studGroupService.AddStudGroupAsync(studGroup);
            return CreatedAtAction(nameof(GetGroupById), new { id = studGroup._id }, studGroup);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudGroup(string id, StudGroup studGroup)
        {
            var existingGroup = await _studGroupService.GetGroupByStringIdAsync(id);

            if (existingGroup is null)
                return BadRequest();

            studGroup._id = existingGroup._id;
            await _studGroupService.UpdateStudGroupAsync(studGroup);

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudGroup(string id)
        {
            var existingStudent = await _studGroupService.GetGroupByStringIdAsync(id);

            if (existingStudent is null)
                return BadRequest();

            await _studGroupService.DeleteStudGroupAsync(id);

            return NoContent();
        }
    }
}
