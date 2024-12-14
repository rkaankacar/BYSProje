using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BYSProje.Models
{
    public class Login
    {   [Key]
        public int UserNo {get; set;}
        public string Password {get; set;}
    }
}