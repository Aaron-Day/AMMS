﻿using AMMS.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.AccountViewModels
{
    public class RequestViewModel
    {
        public RequestViewModel()
        {
            Requested = Formatting.AsMilDateTime(DateTime.UtcNow);
        }

        public string Id { get; set; }

        public string Requested { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^([\w-\.]+)@us.army.mil", ErrorMessage = "Must be a us.army.mil address")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Cell Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Unit")]
        public string Unit { get; set; }
    }
}
