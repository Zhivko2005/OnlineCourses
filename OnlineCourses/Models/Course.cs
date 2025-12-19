using System;

namespace OnlineCourses.Models;

public class Course
{
    public int Id { get; set; }

    public string? Title { get; set; }       
    public string? Description { get; set; }
    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<Assignment>? Assignments { get; set; }
    public ICollection<Enrolment>? Enrollments { get; set; }

    public ICollection<CourseCategory>? CourseCategories { get; set; }
}
