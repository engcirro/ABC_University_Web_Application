using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FictionUniversity.Models
{
    public class Department
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Established Date")]
        public DateTime EstablishedDate { get; set; }

        public int? TeacherID { get; set; }

        public virtual Teacher DepartmentHead { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}