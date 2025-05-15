using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagerment_WebAPI.Model;
using SchoolManagerment_WebAPI.Model.Dto;
using SchoolManagerment_WebAPI.Service;

namespace SchoolManagerment_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : ControllerBase
    {
        private readonly ClassroomService _classroomService;

        public ClassroomController(ClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassrooms()
        {
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return Ok(classroom);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassroom([FromBody] ClassroomGetDto classroom)
        {
            if (classroom == null)
            {
                return BadRequest();
            }
            var createdClassroom = await _classroomService.CreateClassroomAsync(classroom);
            return CreatedAtAction(nameof(GetClassroomById), new { id = createdClassroom.Id }, createdClassroom);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassroom(Guid id, [FromBody] ClassroomGetDto classroom)
        {
            if (classroom == null)
            {
                return BadRequest();
            }
            var updatedClassroom = await _classroomService.UpdateClassroomAsync(id, classroom);
            if (updatedClassroom == null)
            {
                return NotFound();
            }
            return Ok(updatedClassroom);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(Guid id)
        {
            var result = await _classroomService.DeleteClassroomAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}