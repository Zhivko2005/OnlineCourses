using System;

namespace OnlineCourses.Models;

public class Enrolment
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public DateTime EnrolledOn { get; set; }
}