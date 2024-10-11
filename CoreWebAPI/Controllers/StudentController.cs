using CoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DemoContext _context;

        public StudentController(DemoContext context)
        {
            _context = context;
        }


        [HttpPost("AddStudent")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@StudentName", student.StudentName),
                    new SqlParameter("@Class", student.Class),
                    new SqlParameter("@DoB", student.DoB)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC spInsertStudent @StudentName, @Class, @DoB", parameters);

                        //Custom message.
                var response = new
                {
                    status = 200,
                    message = "Data Added Successfully!"
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = "An unexpected error occurred: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                return await _context.Students.FromSqlRaw("SELECT * FROM Student").ToListAsync();  //Students is DbSet property in DemoContext.cs
            }
            catch (Exception ex) 
            {
                var errorResponse = new
                {
                    status = 500,
                    message = "An unexpected error occurred: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var Id = new SqlParameter("@id", id);
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM Student WHERE Id=@id", Id);

                var responseMsg = new
                {
                    status = 200,
                    message = id + ". no Id deleted successfully!"
                };

                return Ok(responseMsg);
            }
            catch(Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = "An unexpected error occurred: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }



        [HttpGet("GetSpecificStudent/{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetSpecificStudent(int id)
        {
            try
            {
                var Id = new SqlParameter("@GetId", id);
                return await _context.Students.FromSqlRaw("SELECT * FROM Student WHERE Id=@GetId", Id).ToListAsync();

            }
            catch(Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = "An unexpected error occurred: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }



        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateStudent(int id, Student student)
        {
            try
            {
                var Id = new[]
                { 
                    new SqlParameter("@id", id),
                    new SqlParameter("@StudentName", student.StudentName),
                    new SqlParameter("@Cls", student.Class),
                    new SqlParameter("@DoB", student.DoB)
                };

                await _context.Database.ExecuteSqlRawAsync("UPDATE Student SET Student_name=@StudentName," +
                    "Class=@Cls, DoB=@DoB WHERE Id=@id", Id);

                var responseMsg = new
                {
                    status = 200,
                    message = id + ". no Id updated successfully!"
                };

                return Ok(responseMsg);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = "An unexpected error occurred: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }





    }
}
