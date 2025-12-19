using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Data;
using OnlineCourses.Models;
namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
         private AppDbContext context;

        public RoleController(AppDbContext context)
        {
            this.context=context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = context.Roles.ToList();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = context.Roles.Find(id);
            if(role == null)
            {
                return NotFound();
            }
            return Ok(role);
        } 
        [HttpPost]
        public IActionResult Create(Role role)
        {
            context.Roles.Add(role);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id=role.Id}, role);    
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Role role)
        {
            if(id != role .Id)
            {
                BadRequest("Wrong ID");
            }
            var existingRole = context.Roles.FirstOrDefault(r => r.Id == id);
            if (existingRole == null)
            {
                return NotFound(); 
            }
            context.Entry(existingRole).CurrentValues.SetValues(role);

            context.SaveChanges();

            return NoContent();
        } 
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Roles.Find(id);

            if (existing == null)
                return NotFound();

            context.Roles.Remove(existing);
            context.SaveChanges();

            return NoContent();
        
        }
    }
}
