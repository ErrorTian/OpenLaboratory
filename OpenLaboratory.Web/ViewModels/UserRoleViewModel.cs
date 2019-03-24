using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenLaboratory.Web.Models;

namespace OpenLaboratory.Web.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public UserRoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
