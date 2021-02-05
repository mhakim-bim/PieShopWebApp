using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.ViewModels
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; }

        
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
    }
}
