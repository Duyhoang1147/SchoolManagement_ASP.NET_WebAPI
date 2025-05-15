using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagerment_WebAPI.Service;
using SchoolManagerment_WebAPI.Model;
using SchoolManagerment_WebAPI.Model.Dto;

namespace SchoolManagerment_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherGetDto teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = createdTeacher.Id }, createdTeacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] TeacherGetDto teacher)
        {
            if (teacher == null || id != teacher.Id)
            {
                return BadRequest();
            }
            var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, teacher);
            if (updatedTeacher == null)
            {
                return NotFound();
            }
            return Ok(updatedTeacher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}