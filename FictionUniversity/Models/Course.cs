using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FictionUniversity.Models
{
    public class Course
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Course Code")]
        public int CourseID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [Range(0, 5)]
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}