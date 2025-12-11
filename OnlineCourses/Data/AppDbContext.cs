using System;

namespace OnlineCourses.Data;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Models;
public class AppDbContext : DbContext
{ 
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<CourseCategory> CourseCategories { get; set; }

    public DbSet<Enrolment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<CourseCategory>()
            .HasKey(cc => new { cc.CourseId, cc.CategoryId });

        modelBuilder.Entity<Enrolment>()
            .HasKey(e => new { e.UserId, e.CourseId });
    }
    

}
