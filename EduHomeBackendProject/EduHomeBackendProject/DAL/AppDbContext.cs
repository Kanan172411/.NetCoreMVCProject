using EduHomeBackendProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventTeachers> EventTeachers { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<SubsribedUsers> subsribedUsers { get; set; } 
        public DbSet<JoinedUser> JoinedUsers { get; set; }
        public DbSet<CourseMessages> CourseMessages { get; set; }
        public DbSet<ContactMessages> ContactMessages { get; set; }
        public DbSet<EventMessages> EventMessages { get; set; }
    }
}
