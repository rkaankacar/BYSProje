using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYSProje.Models
{
    public class StudentCoursesViewModel
    {   
        public int StudentID {get; set;}
        public string First_Name {get; set;}
        public string Last_Name {get; set;}
        
         
        public List<CourseViewModel> Courses { get; set; }

       
    }
}