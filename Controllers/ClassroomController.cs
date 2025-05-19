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
    /// Controller for managing classroom operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClassroomController : ControllerBase
    {
        private readonly ClassroomService _classroomService;

        /// <summary>
        /// Initializes a new instance of the ClassroomController
        /// </summary>
        /// <param name="classroomService">The classroom service dependency</param>
        public ClassroomController(ClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        /// <summary>
        /// Retrieves all classrooms
        /// </summary>
        /// <returns>A list of all classrooms</returns>
        /// <response code="200">Returns the list of classrooms</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClassrooms()
        {
            var classrooms = await _classroomService.GetAllClassroomsAsync();
            return Ok(classrooms);
        }

        /// <summary>
        /// Retrieves a specific classroom by ID
        /// </summary>
        /// <param name="id">The ID of the classroom to retrieve</param>
        /// <returns>The requested classroom</returns>
        /// <response code="200">Returns the requested classroom</response>
        /// <response code="404">If the classroom was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return Ok(classroom);
        }

        /// <summary>
        /// Creates a new classroom
        /// </summary>
        /// <param name="classroom">The classroom data to create</param>
        /// <returns>The created classroom</returns>
        /// <response code="201">Returns the newly created classroom</response>
        /// <response code="400">If the classroom data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateClassroom([FromBody] ClassroomGetDto classroom)
        {
            if (classroom == null)
            {
                return BadRequest();
            }
            var createdClassroom = await _classroomService.CreateClassroomAsync(classroom);
            return CreatedAtAction(nameof(GetClassroomById), new { id = createdClassroom.Id }, createdClassroom);
        }

        /// <summary>
        /// Updates an existing classroom
        /// </summary>
        /// <param name="id">The ID of the classroom to update</param>
        /// <param name="classroom">The updated classroom data</param>
        /// <returns>The updated classroom</returns>
        /// <response code="200">Returns the updated classroom</response>
        /// <response code="400">If the classroom data is invalid</response>
        /// <response code="404">If the classroom was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a specific classroom
        /// </summary>
        /// <param name="id">The ID of the classroom to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the classroom was successfully deleted</response>
        /// <response code="404">If the classroom was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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