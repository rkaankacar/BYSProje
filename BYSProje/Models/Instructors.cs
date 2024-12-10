using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYSProje.Models
{
    public class Instructors
    {
        public int InstructorID {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string  Department  {get; set;}
        public string Password {get; set;}  
        public ICollection<Courses> Courses { get; set; } // navigasyon ilişkileri belirtir
    }
}