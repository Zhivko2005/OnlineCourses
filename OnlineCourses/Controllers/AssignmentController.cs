using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly Data.AppDbContext context;

        public AssignmentController(Data.AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var assignments = context.Assignments.ToList();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var assignment = context.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        [HttpPost]
        public IActionResult Create(Models.Assignment assignment)
        {
            context.Assignments.Add(assignment);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = assignment.Id }, assignment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Assignment assignment)
        {
            if (id != assignment.Id)
            {
                BadRequest("Wrong ID");
            }
            var existingAssignment = context.Assignments.FirstOrDefault(a => a.Id == id);
            if (existingAssignment == null)
            {
                return NotFound();
            }
            context.Entry(existingAssignment).CurrentValues.SetValues(assignment);

            context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var assignment = context.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }
            context.Assignments.Remove(assignment);
            context.SaveChanges();
            return NoContent();
        }
    }
}
