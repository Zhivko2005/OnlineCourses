using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Models;

namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly Data.AppDbContext context;

        public EnrollmentsController(Data.AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var enrollments = context.Enrollments
            .Include(e=>e.User)
            .Include(e=>e.Course)
            .ToList();
            return Ok(enrollments);
        }
        [HttpGet("{id}")]
        public IActionResult GetEnrollmentsForUser(int id)
        {
            var enrollments = context.Enrollments 
            .Where(e=> e.UserId==id)
            .Include(e=>e.User)
            .Include(e=>e.Course)
            .ToList();
            if (enrollments == null)
            {
                return NotFound();
            }
            return Ok(enrollments); 
        }
        public IActionResult Enroll(Enrolment enrollment)
        {
            bool exist = context.Enrollments.Any(e=>e.UserId == enrollment.UserId
            && e.CourseId== enrollment.CourseId);
            if (exist)
            {
                return BadRequest("User already enrolled in this course");
            }
            context.Enrollments.Add(enrollment);
            context.SaveChanges();
            return Ok(enrollment);
        }
    }
}
