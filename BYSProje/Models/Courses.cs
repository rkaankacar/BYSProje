using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYSProje.Models

{
    public class Courses
    {
        public int CourseID { get; set; }
        public string CourseName {get; set;}
        public float Credits {get; set;}
        public int InstructorID {get; set;}
    }
}