using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagerment_WebAPI.Service;
using SchoolManagerment_WebAPI.Model.Dto;

namespace SchoolManagerment_WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing teacher operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        /// <summary>
        /// Initializes a new instance of the TeacherController
        /// </summary>
        /// <param name="teacherService">The teacher service dependency</param>
        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Retrieves all teachers
        /// </summary>
        /// <returns>A list of all teachers</returns>
        /// <response code="200">Returns the list of teachers successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        /// <summary>
        /// Retrieves a specific teacher by their ID
        /// </summary>
        /// <param name="id">The ID of the teacher to retrieve</param>
        /// <returns>The requested teacher data</returns>
        /// <response code="200">Returns the requested teacher</response>
        /// <response code="404">If the teacher was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        /// <summary>
        /// Creates a new teacher
        /// </summary>
        /// <param name="teacher">The teacher data to create</param>
        /// <returns>The created teacher</returns>
        /// <response code="201">Returns the newly created teacher</response>
        /// <response code="400">If the teacher data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherGetDto teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = createdTeacher.Id }, createdTeacher);
        }

        /// <summary>
        /// Updates an existing teacher
        /// </summary>
        /// <param name="id">The ID of the teacher to update</param>
        /// <param name="teacher">The updated teacher data</param>
        /// <returns>The updated teacher</returns>
        /// <response code="200">Returns the updated teacher</response>
        /// <response code="400">If the teacher data is invalid</response>
        /// <response code="404">If the teacher was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] TeacherGetDto teacher)
        {
            if (teacher == null)
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

        /// <summary>
        /// Deletes a specific teacher
        /// </summary>
        /// <param name="id">The ID of the teacher to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the teacher was successfully deleted</response>
        /// <response code="404">If the teacher was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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