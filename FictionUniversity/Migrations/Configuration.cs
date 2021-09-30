namespace ContosoUniversity.Migrations
{
    using FictionUniversity.Models;
    using FictionUniversity.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FictionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FictionContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Arturo",   LastName = "Anand", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Gytis",    LastName = "Barzdukas", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Yan",      LastName = "Li", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Peggy",    LastName = "Justice", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Laura",    LastName = "Norman", Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Nino",     LastName = "Olivetto",  Address="Nairobi, Westgate",
                    RegistrationDate = DateTime.Parse("2005-09-01") }
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var teachers = new List<Teacher>
            {
                new Teacher { FirstName = "Kim",     LastName = "Abercrombie",
                    EmploymentDate = DateTime.Parse("1995-03-11"),Address="Malmo, Sweden" },
                new Teacher { FirstName = "Fadi",    LastName = "Fakhouri",
                    EmploymentDate = DateTime.Parse("2002-07-06"), Address="Malmo, Sweden" },
                new Teacher { FirstName = "Roger",   LastName = "Harui",
                    EmploymentDate = DateTime.Parse("1998-07-01"), Address="Malmo, Sweden"},
                new Teacher { FirstName = "Candace", LastName = "Kapoor",
                    EmploymentDate = DateTime.Parse("2001-01-15"), Address="Malmo, Sweden" },
                new Teacher { FirstName = "Roger",   LastName = "Zheng",
                    EmploymentDate = DateTime.Parse("2004-02-12"), Address="Malmo, Sweden" }
            };
            teachers.ForEach(s => context.Teachers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { DepartmentName = "English",     Budget = 350000,
                    EstablishedDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Abercrombie").ID },
                new Department { DepartmentName = "Mathematics", Budget = 100000,
                    EstablishedDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Fakhouri").ID },
                new Department { DepartmentName = "Engineering", Budget = 350000,
                    EstablishedDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Harui").ID },
                new Department { DepartmentName = "Economics",   Budget = 100000,
                    EstablishedDate = DateTime.Parse("2007-09-01"),
                    TeacherID  = teachers.Single( i => i.LastName == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.DepartmentName, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
                  DepartmentID = departments.Single( s => s.DepartmentName == "Engineering").ID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                  DepartmentID = departments.Single( s => s.DepartmentName == "Economics").ID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                  DepartmentID = departments.Single( s => s.DepartmentName == "Economics").ID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                  DepartmentID = departments.Single( s => s.DepartmentName == "Mathematics").ID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
                  DepartmentID = departments.Single( s => s.DepartmentName == "Mathematics").ID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
                  DepartmentID = departments.Single( s => s.DepartmentName == "English").ID,
                  Teachers = new List<Teacher>()
                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                  DepartmentID = departments.Single( s => s.DepartmentName == "English").ID,
                  Teachers = new List<Teacher>()
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var offices = new List<Office>
            {
                new Office {
                    TeacherID = teachers.Single( i => i.LastName == "Fakhouri").ID,
                    OfficeLocation = "Smith 17" },
                new Office {
                    TeacherID = teachers.Single( i => i.LastName == "Harui").ID,
                    OfficeLocation = "Gowan 27" },
                new Office{
                    TeacherID = teachers.Single( i => i.LastName == "Kapoor").ID,
                    OfficeLocation = "Thompson 304" },
            };
            offices.ForEach(s => context.Offices.AddOrUpdate(p => p.TeacherID, s));
            context.SaveChanges();

            AddOrUpdateTeacher(context, "Chemistry", "Kapoor");
            AddOrUpdateTeacher(context, "Chemistry", "Harui");
            AddOrUpdateTeacher(context, "Microeconomics", "Zheng");
            AddOrUpdateTeacher(context, "Macroeconomics", "Zheng");

            AddOrUpdateTeacher(context, "Calculus", "Fakhouri");
            AddOrUpdateTeacher(context, "Trigonometry", "Harui");
            AddOrUpdateTeacher(context, "Composition", "Abercrombie");
            AddOrUpdateTeacher(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var registrations = new List<Registration>
            {
                new Registration {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Registration {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Registration {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Registration {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Registration {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };

            foreach (Registration e in registrations)
            {
                var registrationInDataBase = context.Registrations.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (registrationInDataBase == null)
                {
                    context.Registrations.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateTeacher(FictionContext context, string courseTitle, string teacherName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Teachers.SingleOrDefault(i => i.LastName == teacherName);
            if (inst == null)
                crs.Teachers.Add(context.Teachers.Single(i => i.LastName == teacherName));
        }
    }
}