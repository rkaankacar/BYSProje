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

        public InstructorController(ILogger<InstructorController> logger)
        {
            _logger = logger;
        }

        public IActionResult AkademisyenSayfasi(string instructorID, string firstName, string lastName, string email, string department)
        {
              var instructorViewModel = new InstructorsViewModel
             {
             InstructorID = instructorID,
               FirstName = firstName,
             LastName = lastName,
                 Email = email,
                Department = department
            };
            return View(instructorViewModel);
        }
         
        public IActionResult ProfilGuncelleme()
        {
            return View();
        }

        [HttpPost]
        public IActionResult updateProfile()
        {
          return View();
        }

        public IActionResult DersAtama()
        {
            return View();
        }

        [HttpPost]
        public IActionResult lessonPost()
        {
           return View();
        }

        public IActionResult DersEkleme()
        {
            return View();
        }

        [HttpPost]
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