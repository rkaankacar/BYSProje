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
        
         [HttpGet("OgrenciSayfasi/{id}")]
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
                      ViewData["StudentID"] = id;
                     return View(model);
        }

        [HttpGet("ProfilGuncelle/{id}")]
        public async Task<IActionResult> ProfilGuncelle(int id)
        {
            
            var student = await _studentService.GetByIDAsync(id);

            if (student == null)
            {
                return NotFound();
            }

           
            var model = new StudentsViewModel
            {
                StudentID = student.StudentID,
                Email = student.Email,
                Major = student.Major
               
            };

            
            return View(model);
        }

        
        [HttpPost("ProfilGuncelle/{id}")]
        public async Task<IActionResult> ProfilGuncelle(int id, StudentsViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var student = await _studentService.GetByIDAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            student.First_Name = model.First_Name;
            student.Last_Name = model.Last_Name;
            student.Email = model.Email;
            student.Password = model.Password;
            
            await _studentService.UpdateAsync(student);

            
            var updatedModel = new StudentsViewModel
            {
                StudentID = student.StudentID,
                Email = student.Email,
                Major = student.Major
            };

         
            TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
            return RedirectToAction("ProfilGuncelle", new { id = student.StudentID });
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
        
             
             [HttpGet("DersGoruntule/{studentid}")]
public async Task<IActionResult> DersGoruntule(int studentId)
{
    // Servisten öğrenciye ait dersleri al
    var studentCourses = await _studentCoursesService.GetCoursesByStudentIdAsync(studentId);

    if (studentCourses == null || !studentCourses.Any())
    {
        return NotFound();
    }
        ViewData["StudentID"] = studentId;
     
    return View(studentCourses); 
}
            


        
        [HttpGet("DersDetay")]
        public IActionResult DersDetay()
        {
            return View();
        }



















        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}