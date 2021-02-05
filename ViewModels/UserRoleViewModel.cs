using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PieShop.Auth;

namespace PieShop.ViewModels
{
    public class UserRoleViewModel
    {
        public string RoleId { get; set; }

        public string UserId { get; set; }

        public  List<ApplicationUser> Users { get; set; }

        public UserRoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
