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
    /// <summary>
    /// Controller for managing teacher-classroom relationships
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherClassroomController : ControllerBase
    {
        private readonly TeacherClassroomService _service;

        /// <summary>
        /// Initializes a new instance of the TeacherClassroomController
        /// </summary>
        /// <param name="service">The teacher-classroom service dependency</param>
        public TeacherClassroomController(TeacherClassroomService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all teacher-classroom relationships
        /// </summary>
        /// <returns>A list of all teacher-classroom assignments</returns>
        /// <response code="200">Returns the list of teacher-classroom relationships</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTeacherClassrooms()
        {
            var teacherClassrooms = await _service.GetAllTeacherClassroomsAsync();
            return Ok(teacherClassrooms);
        }

        /// <summary>
        /// Retrieves all classroom assignments for a specific teacher
        /// </summary>
        /// <param name="id">The ID of the teacher</param>
        /// <returns>List of classroom assignments for the specified teacher</returns>
        /// <response code="200">Returns the teacher's classroom assignments</response>
        /// <response code="404">If the teacher was not found</response>
        [HttpGet("teacher/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacherClassroomByTeacherId(Guid id)
        {
            var teacherClassrooms = await _service.GetTeacherClassroomByTeacherIdAsync(id);
            if (teacherClassrooms == null)
            {
                return NotFound();
            }
            return Ok(teacherClassrooms);
        }

        /// <summary>
        /// Retrieves all teacher assignments for a specific classroom
        /// </summary>
        /// <param name="id">The ID of the classroom</param>
        /// <returns>List of teacher assignments for the specified classroom</returns>
        /// <response code="200">Returns the classroom's teacher assignments</response>
        /// <response code="404">If the classroom was not found</response>
        [HttpGet("classroom/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacherClassroomByClassroomId(Guid id)
        {
            var teacherClassrooms = await _service.GetTeacherClassroomByClassroomIdAsync(id);
            if (teacherClassrooms == null)
            {
                return NotFound();
            }
            return Ok(teacherClassrooms);
        }

        /// <summary>
        /// Creates a new teacher-classroom assignment
        /// </summary>
        /// <param name="teacherClassroom">The teacher-classroom assignment data</param>
        /// <returns>The newly created teacher-classroom assignment</returns>
        /// <response code="201">Returns the newly created assignment</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeacherClassroom([FromBody] TeacherClassroomCreateDto teacherClassroom)
        {
            if (teacherClassroom == null)
            {
                return BadRequest();
            }
            var createdTeacherClassroom = await _service.CreateTeacherClassroomAsync(teacherClassroom);
            return CreatedAtAction(nameof(GetTeacherClassroomByTeacherId), new { id = createdTeacherClassroom.TeacherId }, createdTeacherClassroom);
        }

        /// <summary>
        /// Deletes a teacher-classroom assignment
        /// </summary>
        /// <param name="teacherId">The ID of the teacher</param>
        /// <param name="classroomId">The ID of the classroom</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the assignment was successfully deleted</response>
        /// <response code="404">If the assignment was not found</response>
        [HttpDelete("{teacherId}/{classroomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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