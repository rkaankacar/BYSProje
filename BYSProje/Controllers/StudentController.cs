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
namespace BYSProje.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {   
    private readonly ILogger<StudentController> _logger;
    private readonly IBYSService<Students> _studentService;
    private readonly IBYSService<Student_Courses> _studentCoursesService;
    
    public StudentController(IBYSService<Students> studentService,ILogger<StudentController> logger,IBYSService<Student_Courses> studentCoursesService)
     {
         _studentService = studentService;
          _logger = logger;
          _studentCoursesService = studentCoursesService;
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



        [HttpGet("DersSecimi")]
        public IActionResult DersSecimi()
        {
            return View();
        }

        [HttpPost("DersSecimi")]
        public IActionResult lessonSelect()
        {
            return View();
        }
        
             
          [HttpGet("DersGoruntule/{studentId}")]
public async Task<IActionResult> DersGoruntule(int studentId)
{
   // Öğrenciye ait dersler alınıyor
    var studentCourses = await _studentCoursesService.GetCoursesByStudentIdAsync(studentId);

    if (studentCourses == null || !studentCourses.Any())
    {
        return NotFound(); // Öğrenciye ait ders bulunamadıysa 404 döndür
    }

    // ViewModel oluşturuluyor
    var viewModel = new StudentCoursesViewModel
    {     StudentID = studentId,
        Courses = studentCourses.Select(course => new CourseViewModel
        {  
            CourseID = course.CourseID,
            CourseName = course.CourseName,
            Credits = course.Credits,
            // Diğer gerekli özellikleri buraya ekleyebilirsiniz
        }).ToList()
    };

    return View(viewModel);
}
            











        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}