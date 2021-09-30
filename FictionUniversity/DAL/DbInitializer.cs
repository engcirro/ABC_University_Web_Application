using FictionUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FictionUniversity.DAL
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FictionContext>
    {
        protected override void Seed(FictionContext context)
        {
            var students = new List<Student>
            {
            new Student{FirstName="Mohamud",LastName="Yusuf", Address= "Earth",RegistrationDate=DateTime.Parse("2005-09-01")},
            new Student{FirstName="Israa",LastName="Mohamed", Address="Mars", RegistrationDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Jonathan",LastName="Aiken", Address="Earth",RegistrationDate=DateTime.Parse("2003-09-01")},
            new Student{FirstName="Charles",LastName="Hodson", Address="Mars",RegistrationDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Yilmaz",LastName="Ibrahim", Address="Earth",RegistrationDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Erik",LastName="Toddler", Address="Mars", RegistrationDate=DateTime.Parse("2001-09-01")},
            new Student{FirstName="Roberto",LastName="Diego",Address="Earth", RegistrationDate=DateTime.Parse("2003-09-01")},
            new Student{FirstName="Amino",LastName="Abukar", Address="Earth", RegistrationDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Registration>
            {
            new Registration{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Registration{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Registration{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Registration{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Registration{StudentID=2,CourseID=3141,Grade=Grade.A},
            new Registration{StudentID=2,CourseID=2021,Grade=Grade.B},
            new Registration{StudentID=3,CourseID=1050},
            new Registration{StudentID=4,CourseID=1050,},
            new Registration{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Registration{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Registration{StudentID=6,CourseID=1045},
            new Registration{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Registrations.Add(s));
            context.SaveChanges();
        }
    }

}
