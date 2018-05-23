using System.ComponentModel.DataAnnotations;

namespace AMMS.Models.AccountViewModels
{
    public class RoleListViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Number of Users")]
        public int NumberOfUsers { get; set; }
    }
}
