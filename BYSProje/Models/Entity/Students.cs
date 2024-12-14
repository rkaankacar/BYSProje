using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BYSProje.Models
{
    public class Students
    {   [Key]
        public int StudentID {get; set;}
        public string First_Name {get; set;}
        public string Last_Name {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string Major {get; set;}
         public ICollection<Student_Courses> StudentCourse {get; set;}
    }
}