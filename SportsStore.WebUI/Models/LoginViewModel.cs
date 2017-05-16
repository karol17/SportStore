using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Proszę podać nazwę użytkownika.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Prosze podać hasło.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}