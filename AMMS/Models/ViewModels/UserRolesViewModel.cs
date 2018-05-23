using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.ViewModels
{
    public class UserRolesViewModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "Role Id")]
        public string RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public bool Assigned { get; set; }
    }
}
