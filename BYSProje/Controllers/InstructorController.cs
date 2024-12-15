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
        public InstructorController(ILogger<InstructorController> logger,IBYSService<Instructors> instructorService)
        {
            _logger = logger;
            _instructorService = instructorService;
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
        public IActionResult ProfilGuncelleme()
        {
            return View();
        }

        [HttpPost("ProfilGuncelleme/{id}")]
        public IActionResult updateProfile()
        {
          return View();
        }
        [HttpGet("DersAtama/{id}")]
        public IActionResult DersAtama()
        {
            return View();
        }

        [HttpPost("DersAtama/{id}")]
        public IActionResult lessonPost()
        {
           return View();
        }
        [HttpGet("DersEkleme/{id}")]
        public IActionResult DersEkleme()
        {
            return View();
        }

        [HttpPost("DersEkleme/{id}")]
        public IActionResult lessonAdd()
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