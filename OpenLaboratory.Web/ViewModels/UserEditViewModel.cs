using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OpenLaboratory.Web.Models;

namespace OpenLaboratory.Web.ViewModels
{
    public class UserEditViewModel
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
