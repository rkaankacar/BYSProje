using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BYSProje.Models

{
    public class Courses
    {   [Key]
        public int CourseID { get; set; }
        public string CourseName {get; set;}
        public float Credits {get; set;}
        public int InstructorID {get; set;}
        public string Explanation {get; set;}
        public Instructors? Instructor { get; set; } // navigasyon ilişkiyi belirtir.
        public ICollection<Student_Courses> StudentCourse {get; set;}
    }
}