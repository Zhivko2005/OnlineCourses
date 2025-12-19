using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Data;
using OnlineCourses.Models;
namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private AppDbContext context;
        public UserRoleController(AppDbContext context)
        {
            this.context=context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userRoles = context.UserRoles
            .Include(ur=>ur.User)
            .Include(ur=>ur.Role)
            .ToList();
            return Ok(userRoles);
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetRolesForUser(int userId)
        {
            var roles = context.UserRoles
            .Where(ur=> ur.UserId==userId)
            .Include(ur=> ur.Role)
            .Select(ur=> ur.Role)
            .ToList();
            return Ok(roles);
        }
        [HttpPost]
        public IActionResult AssignRole(UserRole userRole)
        {
            bool exist = context.UserRoles.Any(ur=>ur.UserId == userRole.UserId
            && ur.RoleId== userRole.RoleId);
            if (exist)
            {
                return BadRequest("User already has this role");
            }
            context.UserRoles.Add(userRole);
            context.SaveChanges();
            return Ok(userRole);
        }
        [HttpDelete]
        public IActionResult Delete (UserRole userRole)
        {
            var existing = context.UserRoles
                .FirstOrDefault(ur =>
                ur.UserId == userRole.UserId &&
                ur.RoleId == userRole.RoleId);

            if (existing == null)
                return NotFound();

            context.UserRoles.Remove(existing);
            context.SaveChanges();

            return NoContent();
        }
    }
}
