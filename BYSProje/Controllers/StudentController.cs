using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BYSProje.Models;
using BYSProje.Services;
using BYSProje.DBContext;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
namespace BYSProje.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {   
    private readonly ILogger<StudentController> _logger;
    private readonly IBYSService<Students> _studentService;
    private readonly IBYSService<Student_Courses> _studentCoursesService;
    private readonly IBYSService<Courses> _coursesService;
     private readonly IBYSService<Instructors> _instructorService;
    
    public StudentController(IBYSService<Students> studentService,ILogger<StudentController> logger,IBYSService<Student_Courses> studentCoursesService, IBYSService<Courses> coursesService,IBYSService<Instructors> instructorService)
     {
         _studentService = studentService;
          _logger = logger;
          _studentCoursesService = studentCoursesService;
          _coursesService = coursesService;
          _instructorService = instructorService;
     }
        
        [HttpGet("OgrenciSayfasi")]
        public async Task<IActionResult> OgrenciSayfasi(int id)
        {       
               var student = await _studentService.GetByIDAsync(id);
                 
                  var model = new StudentsViewModel
                    {
                     StudentID = student.StudentID,
                     First_Name = student.First_Name,
                     Last_Name = student.Last_Name,
                     Email = student.Email,
                     Major = student.Major
                    };
                     
                     return View(model);
        }

        [HttpGet("ProfilGuncelle/{id}")]
        public async Task<IActionResult> ProfilGuncelle(int id)
{
    try
    {
        // ID'nin doğru şekilde alındığını kontrol ediyoruz.
        if (id <= 0)
        {
            Console.WriteLine($"Invalid ID received: {id}");
            return BadRequest("Geçersiz ID.");
        }

        // Veritabanından öğrenci bilgisi alınıyor.
        var student = await _studentService.GetByIDAsync(id);
        if (student == null)
        {
            Console.WriteLine($"Student not found with ID: {id}");
            return NotFound("Öğrenci bulunamadı.");
        }

        // Öğrenci bilgisi modele aktarılıyor.
        var model = new StudentsViewModel
        {
            StudentID = student.StudentID,
            First_Name = student.First_Name,
            Last_Name = student.Last_Name,
            Email = student.Email,
            Major = student.Major,
            Password = student.Password
        };

        return View(model);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in GET ProfilGuncelle: {ex.Message}");
        return StatusCode(500, "Sunucu hatası. Lütfen tekrar deneyin.");
    }
}

        [HttpPost("ProfilGuncelle/{id}")]
        public async Task<IActionResult> ProfilGuncelle(int id, StudentsViewModel model)
{      ModelState.Clear();

    try
    {
        // Model doğrulama
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Model validation failed.");
            return View(model);
        }

        // ID'nin doğru şekilde alındığını kontrol ediyoruz.
        if (id <= 0)
        {
            Console.WriteLine($"Invalid ID received: {id}");
            return BadRequest("Geçersiz ID.");
        }

        // Veritabanından öğrenci bilgisi alınıyor.
        var student = await _studentService.GetByIDAsync(id);
        if (student == null)
        {
            Console.WriteLine($"Student not found with ID: {id}");
            return NotFound("Öğrenci bulunamadı.");
        }

        // Modelden gelen veriler öğrenci nesnesine aktarılıyor.
        if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
        {
            Console.WriteLine("Email or Password is empty.");
            ModelState.AddModelError("", "E-posta ve şifre boş olamaz.");
            return View(model);
        }

        student.Email = model.Email;
        student.Password = model.Password;

        // Veritabanında güncelleme işlemi
        await _studentService.UpdateAsync(student);
        Console.WriteLine($"Student updated successfully: {student.StudentID}");

        // Başarı mesajı ve yönlendirme
        TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
        return RedirectToAction("ProfilGuncelle", new { id = student.StudentID });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in POST ProfilGuncelle: {ex.Message}");
        ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
        return View(model);
    }
}

        [HttpGet("DersSecim/{id}")]
        public async Task<IActionResult> DersSecim(int id)
        {   
         
          var student = await _studentService.GetByIDAsync(id); // Öğrenci bilgisi kontrolü
          if (student == null)
          {
            return NotFound("Öğrenci bulunamadı.");
          }

          var selectedCourses = await _studentCoursesService.GetCoursesByStudentIdAsync(id);
        var allCourses = await _coursesService.GetAllCoursesAsync();
          
          var SCourses = allCourses
         .Where(c => !selectedCourses.Any(sc => sc.CourseID == c.CourseID))
         .Select(c => new Courses
         {
            CourseID = c.CourseID,
            CourseName = c.CourseName,
            Instructor = c.Instructor
         })
         .ToList();
         var model = new DersSecimiViewModel
          {
           StudentID = student.StudentID,
           SCourses = SCourses
          };

          return View(model);
        }

        [HttpPost("DersSecim/{id}")]
        public async Task<IActionResult> lessonSelect()
        {
            return View();
        }
        
        [HttpGet("DersGoruntule/{studentId}")]
        public async Task<IActionResult> DersGoruntule(int studentId)
{
    // Öğrencinin derslerini al
    var studentCourses = await _studentCoursesService.GetCoursesByStudentIdAsync(studentId);

    if (studentCourses == null || !studentCourses.Any())
    {
        return NotFound(); // Öğrenciye ait ders bulunamazsa 404 döndür
    }

    // Veriyi model ile view'a aktarma
    var viewModel = new StudentCoursesViewModel
    {
        StudentID = studentId,
        Courses = studentCourses.Select(course => new CourseViewModel
        {  
            CourseID = course.CourseID,
            CourseName = course.CourseName,
            Credits = course.Credits,
        }).ToList()
    };

    return View(viewModel);
}
            
        [HttpGet("DersDetay/{courseId}")]
        public async Task<IActionResult> DersDetay(int courseId)
    {
        // Kursu ve ilişkili bilgileri alıyoruz
        var course = await _coursesService.GetByIDAsync(courseId);

        if (course == null)
        {
            return NotFound(); // Kurs bulunamazsa 404 döndür
        }

        // Instructor bilgilerini alıyoruz
        var instructor = await _instructorService.GetByIDAsync(course.InstructorID);

        // Öğrenci ders bilgilerini alıyoruz
        var studentCourses = await _studentCoursesService.GetAllAsync();

        // ViewModel'e dönüştürüyoruz
        var courseViewModel = new CourseViewModel
        {
            CourseID = course.CourseID,
            CourseName = course.CourseName,
            Credits = course.Credits,
            InstructorID = course.InstructorID,
            FirstName = instructor?.FirstName,  
            LastName = instructor?.LastName,   
            Explanation = course.Explanation,
            StudentCourse = studentCourses.Where(sc => sc.CourseID == courseId).ToList(),
            StudentID = studentCourses.FirstOrDefault()?.StudentID ?? 0 
        };

        return View(courseViewModel);
    }
     
    













        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}