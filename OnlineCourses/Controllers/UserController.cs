using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Data;
using OnlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AppDbContext context;

        public UserController(AppDbContext context)
        {
            this.context=context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = context.Users.ToList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = context.Users.Find(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        } 
        [HttpPost]
        public IActionResult Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id=user.Id}, user);    
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            if(id != user.Id)
            {
                BadRequest("Wrong ID");
            }
            var existingUser = context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(); 
            }
            context.Entry(existingUser).CurrentValues.SetValues(user);

            context.SaveChanges();

            return NoContent();
        } 
        
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Users.Find(id);

            if (existing == null)
                return NotFound();

            context.Users.Remove(existing);
            context.SaveChanges();

            return NoContent();
        
        }
    }
}
