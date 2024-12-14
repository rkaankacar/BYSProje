using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BYSProje.Models
{
    public class StudentsViewModel
    {
        public int StudentID {get; set;}
        [Required(ErrorMessage = "Ad boş olamaz.")]
        public string First_Name {get; set;}
        [Required(ErrorMessage = "Soyad boş olamaz.")]
        public string Last_Name {get; set;}
         [Required(ErrorMessage = "E-posta boş olamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email {get; set;}
        public string Major {get; set;}

         [Required(ErrorMessage = "Şifre boş olamaz.")]
         [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password {get; set;}

       
    }
}