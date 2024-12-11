using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYSProje.Models;
using BYSProje.Services;
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
            var studentViewModel = new StudentsViewModel
            {
                StudentID = student.StudentID.ToString(),
                First_Name = student.First_Name,
                Last_Name = student.Last_Name,
                Email = student.Email,
                Major = student.Major
            };
            return View("~/Views/Student/OgrenciSayfasi.cshtml", studentViewModel);
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
            var instructorViewModel = new InstructorsViewModel
            {
                  InstructorID = instructor.InstructorID.ToString(),
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Email = instructor.Email,
                    Department = instructor.Department
            };
            return View("~/Views/Instructor/AkademisyenSayfasi.cshtml", instructorViewModel);
        }
        else
        {
            return View("Index", login);
        }
    }

    return BadRequest("Invalid form ID.");
}
    


     public IActionResult Error()
{
    var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    var errorViewModel = new ErrorViewModel { RequestId = requestId };
    return View(errorViewModel);
}
}







