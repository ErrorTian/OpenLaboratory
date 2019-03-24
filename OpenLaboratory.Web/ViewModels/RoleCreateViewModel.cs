using System.ComponentModel.DataAnnotations;

namespace OpenLaboratory.Web.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required]
        [Display(Name="角色名称")]
        public string RoleName { get; set; }
    }
}