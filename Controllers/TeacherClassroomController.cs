using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagerment_WebAPI.Data;
using SchoolManagerment_WebAPI.Model;
using SchoolManagerment_WebAPI.Service;

namespace SchoolManagerment_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherClassroomController : ControllerBase
    {
        private readonly TeacherClassroomService _service;

        public TeacherClassroomController(TeacherClassroomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeacherClassrooms()
        {
            var teacherClassrooms = await _service.GetAllTeacherClassroomsAsync();
            return Ok(teacherClassrooms);
        }

        [HttpGet("teacher/{id}")]
        public async Task<IActionResult> GetTeacherClassroomByTeacherId(Guid id)
        {
            var teacherClassrooms = await _service.GetTeacherClassroomByTeacherIdAsync(id);
            if (teacherClassrooms == null || !teacherClassrooms.Any())
            {
                return NotFound();
            }
            return Ok(teacherClassrooms);
        }

        [HttpGet("classroom/{id}")]
        public async Task<IActionResult> GetTeacherClassroomByClassroomId(Guid id)
        {
            var teacherClassrooms = await _service.GetTeacherClassroomByClassroomIdAsync(id);
            if (teacherClassrooms == null || !teacherClassrooms.Any())
            {
                return NotFound();
            }
            return Ok(teacherClassrooms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacherClassroom([FromBody] TeacherClassroom teacherClassroom)
        {
            if (teacherClassroom == null)
            {
                return BadRequest();
            }
            var createdTeacherClassroom = await _service.CreateTeacherClassroomAsync(teacherClassroom);
            return CreatedAtAction(nameof(GetTeacherClassroomByTeacherId), new { id = createdTeacherClassroom.TeacherId }, createdTeacherClassroom);
        }

        [HttpDelete("{teacherId}/{classroomId}")]
        public async Task<IActionResult> DeleteTeacherClassroom(Guid teacherId, Guid classroomId)
        {
            var result = await _service.DeleteTeacherClassroomAsync(teacherId, classroomId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}