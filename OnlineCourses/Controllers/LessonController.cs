using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly Data.AppDbContext context;

        public LessonController(Data.AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lessons = context.Lessons.ToList();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var lesson = context.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson);
        }

        [HttpPost]
        public IActionResult Create(Models.Lesson lesson)
        {
            context.Lessons.Add(lesson);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = lesson.Id }, lesson);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Lesson lesson)
        {
            if (id != lesson.Id)
            {
                BadRequest("Wrong ID");
            }
            var existingLesson = context.Lessons.FirstOrDefault(l => l.Id == id);
            if (existingLesson == null)
            {
                return NotFound();
            }
            context.Entry(existingLesson).CurrentValues.SetValues(lesson);

            context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}") ]
        public IActionResult Delete(int id)
        {
            var lesson = context.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }
            context.Lessons.Remove(lesson);
            context.SaveChanges();
            return NoContent();
        }
    }
}
