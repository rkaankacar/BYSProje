using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYSProje.Models;
using BYSProje.Services;
using System.Data.Common;
namespace BYSProje.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBYSService<Students> _studentService;
    private readonly IBYSService<Instructors> _instructorService;
    public HomeController(IBYSService<Students> studentService, IBYSService<Instructors> instructorService,ILogger<HomeController> logger)
{
    _studentService = studentService;
    _instructorService = instructorService;
     _logger = logger;
}
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SubmitForm (int formID, Login login)
    {  
         if (formID == 1) // Öğrenci için işlem
    {
        var student = await _studentService.GetByConditionAsync(login.UserNo, login.Password);
          if (student != null)
        {     
            
            
            return RedirectToAction("OgrenciSayfasi", "Student",new { id = student.StudentID });
        }
        else
        {
            return View("Index", login);
        }
    }
    else if (formID == 2) // Akademisyen için işlem
    {
        var instructor = await _instructorService.GetByConditionAsync(login.UserNo, login.Password);

        if (instructor != null)
        {
           
            return RedirectToAction("AkademisyenSayfasi", "Instructor", new { id = instructor.InstructorID } );
        }
        else
        {
            return View("Index", login);
        }
    }

    return BadRequest("Invalid form ID.");
}
    

     [HttpGet("SifreSifirla")]
     public IActionResult SifreSifirla()
     {
           return View("SifreSifirla");
     }

     [HttpPost("SifreSifirla")]
     public async Task<IActionResult> SifreSifirla(int formID, Login login)
     {     
        if(formID == 1)
        {
              var student = await _studentService.GetByIDAsync(login.UserNo);
              if(student.StudentID == login.UserNo)
              {    
                    student.Password = login.Password;
                   await _studentService.UpdateAsync(student);
                   RedirectToAction("Index");
              }
        }
        else if(formID == 2)
        {
             var instructor = await _instructorService.GetByIDAsync(login.UserNo);
             if(instructor.InstructorID==login.UserNo)
             {
                instructor.Password = login.Password;
                await _instructorService.UpdateAsync(instructor);
                RedirectToAction("Index");
             }
        }
           return View(login);
     }
     public IActionResult Error()
{
    var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    var errorViewModel = new ErrorViewModel { RequestId = requestId };
    return View(errorViewModel);
}
}







