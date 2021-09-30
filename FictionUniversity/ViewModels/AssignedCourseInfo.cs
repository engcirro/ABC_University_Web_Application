using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FictionUniversity.ViewModels
{
    public class AssignedCourseInfo
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public bool HasBeenAssigned { get; set; }
    }
}