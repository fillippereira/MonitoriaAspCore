using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoriaAspCore.Areas.Administracao.Models
{
    public class User
    {   
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }
        [Required]

        [Display(Name = "Login")]
        public string UserName { get; set; }
        [Required]

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Gênereo")]
        public string Gender { get; set; }


        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        
    }
}
