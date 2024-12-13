using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace BYSProje.Models
{
    public class CourseViewModel
    {   
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public float Credits { get; set; }
        public Courses Course { get; set; }
        public Students Student { get; set; }
        
    }
}