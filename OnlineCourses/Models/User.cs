using System;

namespace OnlineCourses.Models;

public class User
{
    public int Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        // Navigation
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Enrolment> Enrollments { get; set; }
        public ICollection<Submission> Submissions { get; set; }
}
