using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FictionUniversity.Models
{
    public class Office
    {
        [Key]
        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string OfficeLocation { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}