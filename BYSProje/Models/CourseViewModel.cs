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
    public int InstructorID { get; set; }
    public string FirstName { get; set; } // Öğretmenin adı
    public string LastName { get; set; }  // Öğretmenin soyadı
    public string InstructorName => $"{FirstName} {LastName}"; // İsim + Soyisim birleşimi
    public string Explanation { get; set; }
    public ICollection<Student_Courses> StudentCourse { get; set; }
     public int StudentID {get;set;}//ogrencidi vardı.
    }
}