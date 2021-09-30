using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using FictionUniversity.Models;

namespace FictionUniversity.DAL
{
    public class FictionContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Office> Offices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Teachers).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("TeacherID")
                    .ToTable("CourseTeacher"));

            /* USING FLUENT API INSTEAD OF ATTRIBUTES TO JOIN COURSES AND TEACHERS
             modelBuilder.Entity<Instructor>()
    .HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
            */
        }
        /* public FictionContext() : base("FictionContext")
         {
         }

         public DbSet<Student> Students { get; set; }
         public DbSet<Registration> Registrations { get; set; }
         public DbSet<Course> Courses { get; set; }

         protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
             modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         }*/
        }

    }
