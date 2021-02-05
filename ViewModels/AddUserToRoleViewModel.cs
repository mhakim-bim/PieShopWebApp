using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PieShop.Auth;

namespace PieShop.ViewModels
{
    public class AddUserToRoleViewModel
    {
        public string RoleId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
