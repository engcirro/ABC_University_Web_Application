using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FictionUniversity.ViewModels
{
    public class RegistrationDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }

        public int NumberOfStudents { get; set; }
    }
}