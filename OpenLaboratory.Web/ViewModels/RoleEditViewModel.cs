using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace OpenLaboratory.Web.ViewModels
{
    public class RoleEditViewModel
    {
        public string Id { get; set; }
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        public Dictionary<string,string> Users { get; set; }

        public RoleEditViewModel()
        {
            Users=new Dictionary<string, string>();
        }
    }
}
