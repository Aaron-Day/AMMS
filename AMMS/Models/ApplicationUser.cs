using AMMS.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMMS.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        private static readonly string[] ranks = new[]
        {
            "PVT", "PV2", "PFC", "SPC", "CPL", "SGT", "SSG", "SFC", "MSG", "1SG", "SGM", "CSM", "SMA",
            "WO1", "CW2", "CW3", "CW4", "CW5",
            "2LT", "1LT", "CPT", "MAJ", "LTC", "COL", "BG", "MG", "LTG", "GEN", "GA"
        };

        public ApplicationUser()
        {
            EmailConfirmed = true;
            TwoFactorEnabled = false;
            ActiveEntries = false;
            Closed = null;
            Created = Formatting.AsMilDateTime(DateTime.UtcNow);
            LastActive = Formatting.AsMilDateTime(DateTime.UtcNow);
            Salt = PasswordProtocol.PasswordSalt;
            // ConcurrencyStamp (nvarchar(max), null)
            /* page loads with stamp,
             * page saves (updates db) if db stamp matches loaded stamp,
             * if save succeeds then the stamp updates
             * if save fails then another user already modified the page
             */
        }

        public string Salt { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "SSN")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Invalid Social Security Number")]
        public string SocialSecurityNumber { get; set; }

        [StringLength(3, MinimumLength = 2)]
        [RegularExpression(@"^\w{3}$", ErrorMessage = "Invalid Rank")]
        [Compare("ranks", ErrorMessage = "Invalid rank")]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "DOB")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Account Created")]
        public string Created { get; set; }

        // DateTime the user last did anything with account
        [Display(Name = "Last Active")]
        public string LastActive { get; set; }

        [Display(Name = "Account Closed")]
        public string Closed { get; set; }

        // Returns true if there are any flights, faults, or other documents still on record
        [Display(Name = "Active")]
        public bool ActiveEntries { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^([\w-\.]+)@us.army.mil", ErrorMessage = "Must be a us.army.mil address")]
        public override string Email { get; set; }

        [Required]
        [Display(Name = "UIC")]
        public string AssignedUnit { get; set; }
    }
}
