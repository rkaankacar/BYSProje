using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BYSProje.Models
{
    public class CourseViewModel
    {   
    public int CourseID { get; set; }

    [Required(ErrorMessage = "Ders adı gereklidir.")]
    [StringLength(100, ErrorMessage = "Ders adı 100 karakterden uzun olamaz.")]
    public string CourseName { get; set; }

     [Range(1, 10, ErrorMessage = "Kredi 1 ile 10 arasında olmalıdır.")]
    public float Credits { get; set; }
    public int InstructorID { get; set; }
    public string FirstName { get; set; } // Öğretmenin adı
    public string LastName { get; set; }  // Öğretmenin soyadı
    public string InstructorName => $"{FirstName} {LastName}"; // İsim + Soyisim birleşimi
    [Required(ErrorMessage = "Açıklama gereklidir.")]
    public string Explanation { get; set; }
    public ICollection<Student_Courses> StudentCourse { get; set; }
    public int StudentID {get; set;}
  
    }
}