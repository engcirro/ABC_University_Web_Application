using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FictionUniversity.DAL;
using FictionUniversity.Models;
using FictionUniversity.ViewModels;

namespace FictionUniversity.Controllers
{
    public class TeacherController : Controller
    {
        private FictionContext db = new FictionContext();

        // GET: Teacher
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new TeacherIndexData();
            viewModel.Teachers = db.Teachers
                .Include(i => i.Office)
                .Include(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.TeacherID = id.Value;
                viewModel.Courses = viewModel.Teachers.Where(
                    i => i.ID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                db.Entry(selectedCourse).Collection(x => x.Registrations).Load();
                foreach (Registration enrollment in selectedCourse.Registrations)
                {
                    db.Entry(enrollment).Reference(x => x.Student).Load();
                }

                viewModel.Registrations = selectedCourse.Registrations;
                viewModel.Registrations = viewModel.Courses.Where(
                    x => x.CourseID == courseID).Single().Registrations;
            }

            return View(viewModel);
        }
        /* public ActionResult Index()
         {
             var teachers = db.Teachers.Include(t => t.Office); //The scafolded code of this method specifies Eager loading of the Office nav property
             return View(teachers.ToList());
         }
 */
        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            var Teacher = new Teacher();
            Teacher.Courses = new List<Course>();
            PopulateAssignedCourseInfo(Teacher);
            //ViewBag.ID = new SelectList(db.Offices, "TeacherID", "OfficeLocation");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,EmploymentDate,Office")] Teacher teacher, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                teacher.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    teacher.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseInfo(teacher);
            return View(teacher);

            /* [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "ID,LastName,FirstName,EmploymentDate,Address")] Teacher teacher)
            {
                if (ModelState.IsValid)
                {
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ID = new SelectList(db.Offices, "TeacherID", "OfficeLocation", teacher.ID);
                return View(teacher);
            }*/
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers
                .Include(i => i.Office)
                .Include(i => i.Courses)
                .Where(i => i.ID==id)
                .Single();
            PopulateAssignedCourseInfo(teacher);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ID = new SelectList(db.Offices, "TeacherID", "OfficeLocation", teacher.ID);
            return View(teacher);
        }
        private void PopulateAssignedCourseInfo(Teacher teacher)
        {
            var allCourses = db.Courses;
            var instructorCourses = new HashSet<int>(teacher.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseInfo>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseInfo
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    HasBeenAssigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        //public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teacherToUpdate = db.Teachers
               .Include(i => i.Office)
               .Include(i => i.Courses)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(teacherToUpdate, "",
               new string[] { "LastName", "FirstName", "EmploymentDate", "Address", "Office" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(teacherToUpdate.Office.OfficeLocation))
                    {
                        teacherToUpdate.Office = null;
                    }
                    UpdateTeacherCourses(selectedCourses, teacherToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseInfo(teacherToUpdate);
            return View(teacherToUpdate);
        }
        private void UpdateTeacherCourses(string[] selectedCourses, Teacher teacherToUpdate)
        {
            if (selectedCourses == null)
            {
                teacherToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (teacherToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        teacherToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        teacherToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
        /*        [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,EmploymentDate,Address")] Teacher teacher)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(teacher).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.ID = new SelectList(db.Offices, "TeacherID", "OfficeLocation", teacher.ID);
                    return View(teacher);
                }*/


        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers
               .Include(i => i.Office)
               .Where(i => i.ID == id)
               .Single();
            db.Teachers.Remove(teacher); //Enabling Deletion of a teacher with administrator privilidge
            var department = db.Departments
              .Where(d => d.TeacherID == id)
              .SingleOrDefault();
            if (department != null)
            {
                department.TeacherID = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
