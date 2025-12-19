using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private Data.AppDbContext context;
        public CourseCategoryController(Data.AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var courseCategories = context.CourseCategories
            .Include(cc => cc.Course)
            .Include(cc => cc.Category)
            .ToList();
            return Ok(courseCategories);
        }
        [HttpGet("course/{courseId}")]
        public IActionResult GetCategoriesForCourse(int courseId)
        {
            var categories = context.CourseCategories
            .Where(cc => cc.CourseId == courseId)
            .Include(cc => cc.Category)
            .Select(cc => cc.Category)
            .ToList();
            return Ok(categories);
        }
        [HttpPost]
        public IActionResult AssignCategory(Models.CourseCategory courseCategory)
        {
            bool exist = context.CourseCategories.Any(cc => cc.CourseId == courseCategory.CourseId
            && cc.CategoryId == courseCategory.CategoryId);
            if (exist)
            {
                return BadRequest("Course already has this category");
            }
            context.CourseCategories.Add(courseCategory);
            context.SaveChanges();
            return Ok(courseCategory);
        }
        [HttpDelete]
        public IActionResult Delete(Models.CourseCategory courseCategory)
        {
            var existing = context.CourseCategories
                .FirstOrDefault(cc =>
                cc.CourseId == courseCategory.CourseId &&
                cc.CategoryId == courseCategory.CategoryId);

            if (existing == null)
                return NotFound();

            context.CourseCategories.Remove(existing);
            context.SaveChanges();

            return NoContent();
        }
    }
}
