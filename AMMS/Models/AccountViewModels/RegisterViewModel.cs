using System;
using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        public string Rank { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^([\w-\.]+)@us.army.mil", ErrorMessage = "Must be a us.army.mil address")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Cell Phone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yymmdd}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "SSN")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Invalid Social Security Number")]
        public string SocialSecurityNumber { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "UIC")]
        public string AssignedUnit { get; set; }

        public string Pid => $"{FirstName[0]}{LastName[0]}{SocialSecurityNumber.Substring(SocialSecurityNumber.Length - 4)}";
    }
}
