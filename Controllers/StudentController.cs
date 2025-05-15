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
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCUDDot student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            var createdStudent = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentCUDDot student)
        {
            try {
                if (student == null)
                {
                    return BadRequest("Student ID mismatch or invalid student data");
                }
                var updatedStudent = await _studentService.UpdateStudentAsync(id, student);
                if (updatedStudent == null)
                {
                    return NotFound("Student not found");
                }
                return Ok(updatedStudent);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}