using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Data;

namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private AppDbContext context;
        public CourseController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = context.Courses.ToList();
            return Ok(courses);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        [HttpPost]
        public IActionResult Create(Models.Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Course course)
        {
            if (id != course.Id)
            {
                BadRequest("Wrong ID");
            }
            var existingCourse = context.Courses.FirstOrDefault(c => c.Id == id);
            if (existingCourse == null)
            {
                return NotFound();
            }
            context.Entry(existingCourse).CurrentValues.SetValues(course);

            context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingCourse = context.Courses.FirstOrDefault(c => c.Id == id);
            if (existingCourse == null)
            {
                return NotFound();
            }
            context.Courses.Remove(existingCourse);
            context.SaveChanges();
            return NoContent();
        }
    }
}
