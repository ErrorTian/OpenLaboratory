using System.ComponentModel.DataAnnotations;

namespace OpenLaboratory.Web.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [Display(Name = "学号")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string StudentName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
