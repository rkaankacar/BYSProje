using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
namespace BYSProje.Models
{
    public class Student_Courses
    {   [Key]
        public int StudentID {get; set;}
        public int CourseID {get; set;}
         
        public bool IsApproved { get; set; }
        public virtual Students? Student {get; set;}
        public virtual Courses? Course {get; set;} 
    }
}