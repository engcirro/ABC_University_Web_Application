using FictionUniversity.DAL;
using FictionUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FictionUniversity.Controllers
{
    public class HomeController : Controller
    {
        private FictionContext db = new FictionContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           /* ViewBag.Message = "Your application description page.";

            return View();*/
           IQueryable<RegistrationDateGroup> data = from student in db.Students
               group student by student.RegistrationDate into dateGroup
               select new RegistrationDateGroup()
               {
                   RegistrationDate = dateGroup.Key,
                   NumberOfStudents = dateGroup.Count()
               };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}