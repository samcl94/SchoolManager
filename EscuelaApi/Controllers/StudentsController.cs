using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{dni}")]
        public async Task<ActionResult<Student>> GetStudentByDni(string dni)
        {
            try
            {
                var student = await _studentService.GetStudentByDniAsync(dni);
                return student != null
                    ? Ok(student)
                    : NotFound(new { error = $"Student {dni} not found" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(
            [FromQuery] string? dni, [FromQuery] int? birthYear,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var students = await _studentService.GetStudentsAsync(dni, birthYear, page, pageSize);
                return Ok(students);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Student>>> CreateStudents([FromBody] IEnumerable<Student> students)
        {
            try
            {
                var created = await _studentService.CreateStudentsAsync(students);
                return Created("", created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpDelete("{dni}")]
        public async Task<ActionResult> DeleteStudent(string dni)
        {
            try
            {
                var deleted = await _studentService.DeleteStudentAsync(dni);

                if (!deleted)
                    return NotFound(new { error = $"Student {dni} not found." });

                return Ok(new { message = $"Student {dni} deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}

