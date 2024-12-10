using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace BYSProje.Models
{
    public class Student_Courses
    {
        public int StudentID {get; set;}
        public int CourseID {get; set;}

        public virtual Students? Student {get; set;}
        public virtual Courses? Course {get; set;} 
    }
}