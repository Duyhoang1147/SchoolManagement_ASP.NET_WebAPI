using Microsoft.AspNetCore.Mvc;
using SchoolManagerment_WebAPI.Service;
using SchoolManagerment_WebAPI.Model.Dto;

namespace SchoolManagerment_WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing student operations
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints to:
    /// - Get all students
    /// - Get student by ID
    /// - Create new student
    /// - Update existing student
    /// - Delete student
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        /// <summary>
        /// Initializes a new instance of the StudentController
        /// </summary>
        /// <param name="studentService">The student service dependency</param>
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Retrieves all students from the database
        /// </summary>
        /// <returns>A list of all students</returns>
        /// <response code="200">Returns the list of students successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Retrieves a specific student by their ID
        /// </summary>
        /// <param name="id">The ID of the student to retrieve</param>
        /// <returns>The requested student data</returns>
        /// <response code="200">Returns the requested student</response>
        /// <response code="404">If the student was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <param name="student">The student data to create</param>
        /// <returns>The created student</returns>
        /// <response code="201">Returns the newly created student</response>
        /// <response code="400">If the student data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCUDDot student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            var createdStudent = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
        }

        /// <summary>
        /// Updates an existing student
        /// </summary>
        /// <param name="id">The ID of the student to update</param>
        /// <param name="student">The updated student data</param>
        /// <returns>The updated student</returns>
        /// <response code="200">Returns the updated student</response>
        /// <response code="400">If the student data is invalid</response>
        /// <response code="404">If the student was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentCUDDot student)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a specific student
        /// </summary>
        /// <param name="id">The ID of the student to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the student was successfully deleted</response>
        /// <response code="404">If the student was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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