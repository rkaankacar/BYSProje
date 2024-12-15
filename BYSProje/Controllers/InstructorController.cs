using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BYSProje.Models;
using Microsoft.Identity.Client;
using BYSProje.Services;
namespace BYSProje.Controllers
{
    [Route("[controller]")]
    public class InstructorController : Controller
    {
        private readonly ILogger<InstructorController> _logger;
         private readonly IBYSService<Instructors> _instructorService;
          private readonly IBYSService<Courses> _courseService;
          private readonly IBYSService<Student_Courses> _studentcourseService;
         
        public InstructorController(ILogger<InstructorController> logger,IBYSService<Instructors> instructorService,IBYSService<Courses> courseService, IBYSService<Student_Courses> studencourseService)
        {
            _logger = logger;
            _instructorService = instructorService;
            _courseService = courseService;
            _studentcourseService = studencourseService;
            
        }
        
        [HttpGet("AkademisyenSayfasi")]
        public async Task<ActionResult> AkademisyenSayfasi(int id)
        {    
            var instructor = await _instructorService.GetByIDAsync(id);
            var model = new InstructorsViewModel
              {  InstructorID = instructor.InstructorID,
                 Department = instructor.Department,
                 FirstName = instructor.FirstName,
                 LastName = instructor.LastName,
                 Email = instructor.Email
              };
            return View("AkademisyenSayfasi",model);
        }
        
        [HttpGet("ProfilGuncelleme/{id}")]
        public async Task<IActionResult> ProfilGuncelleme(int id)
        {   var instructor = await _instructorService.GetByIDAsync(id);
            
              var model = new InstructorsViewModel
        {
            InstructorID = instructor.InstructorID,
            FirstName = instructor.FirstName,
            LastName = instructor.LastName,
            Email = instructor.Email,
            Department = instructor.Department,
            Password = instructor.Password
        };
            return View(model);
        }

        [HttpPost("ProfilGuncelleme/{id}")]
        public async Task<IActionResult> ProfilGuncelleme(int id, InstructorsViewModel model)
        {
             ModelState.Clear();

             var instructor = await _instructorService.GetByIDAsync(id);
              instructor.Email = model.Email;
              instructor.Password = model.Password;

              await _instructorService.UpdateAsync(instructor);

             TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
             return RedirectToAction("ProfilGuncelleme", new {id = instructor.InstructorID});
        }

        [HttpGet("DersEkleme/{id}")]
        public async Task<IActionResult> DersEkleme(int id)
        {  var instructor = await _instructorService.GetByIDAsync(id);
              if (instructor == null)
              {
                  TempData["ErrorMessage"] = "Eğitmen bulunamadı.";
                  return RedirectToAction("ErrorPage"); // Hata sayfasına yönlendirme yapılabilir
              }

            var course = new CourseViewModel
              {
                  InstructorID = instructor.InstructorID
              };

                    return View(course);
        }

        [HttpPost("DersEkleme/{id}")]
        public async Task<IActionResult> DersEkleme(int id, CourseViewModel model)
{
    

    if (!ModelState.IsValid)
    {
        await _courseService.AddAsync(new Courses
        {
            CourseName = model.CourseName,
            Credits = model.Credits,
            Explanation = model.Explanation,
            InstructorID = model.InstructorID
        });

        TempData["SuccessMessage"] = "Ders başarıyla eklendi!";
        return RedirectToAction("DersEkleme",new { id = model.InstructorID } );
    }

    TempData["ErrorMessage"] = "Veri doğrulama hatası oluştu.";
    return View(model);
}
      
        
         
   
          
    
    
  }



}
