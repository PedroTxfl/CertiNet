﻿using System.ComponentModel.DataAnnotations;

namespace CertiNet1.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Perfil")]
        public string RoleName { get; set; }
    }
}
