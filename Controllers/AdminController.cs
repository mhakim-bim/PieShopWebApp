using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PieShop.Auth;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    [Authorize(Roles= "Administrators")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole identityRole)
        {
            if (!ModelState.IsValid) return View(identityRole);

            var result = await _roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }
            ModelState.AddModelError("","Error in Role Name");

            return View(identityRole);
        }

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return RedirectToAction("RoleManagement", _roleManager.Roles);

            var userRoleViewModel = new UserRoleViewModel() {RoleId = role.Id};

            foreach (var user in _userManager.Users)
            {
                if (! await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userRoleViewModel.Users.Add(user);
                }
            }

            return View("AddUserToRole", userRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid) return View(userRoleViewModel);

            var role =  await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);

            if (role != null && user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    userRoleViewModel.Users.Add(user);
                }
                return View("AddUserToRole", userRoleViewModel);
            }

            return View("RoleManagement", _roleManager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var result =  await  _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                  return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            ModelState.AddModelError("","Role can not be deleted");
            return RedirectToAction("RoleManagement", _roleManager.Roles);

        }

        
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var editRoleViewModel = new EditRoleViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            //Get Users in Role
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    editRoleViewModel.Users.Add(user.UserName);
                }
            }


            return View(editRoleViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateRole(EditRoleViewModel editRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(editRoleViewModel.RoleId);

            if (role != null)
            {
                role.Name = editRoleViewModel.RoleName;
                
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement",_roleManager.Roles);
                }

                ModelState.AddModelError("", "Role not updated,something wnt wrong");
                return View(editRoleViewModel);
            }

            return RedirectToAction("RoleManagement", _roleManager.Roles);
        }



        public IActionResult UserManagement()
        {
            var users = _userManager.Users;

            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid)
                return View(addUserViewModel);

            var user = new ApplicationUser()
            {
                UserName = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
                BirthDate = addUserViewModel.BirthDate,
                City = addUserViewModel.City,
                Country = addUserViewModel.Country
            };

            var result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("UserManagement",_userManager.Users);
            }

            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError("",identityError.Description);
            }

            return View(addUserViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
              var result = await _userManager.DeleteAsync(user);
            }

            return View("UserManagement", _userManager.Users);
        }



        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id ,string userName , string email,
            DateTime birth,string city ,string country )
        {
            var user =  await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.UserName = userName;
                user.Email = email;
                user.BirthDate = birth;
                user.City = city;
                user.Country = country;
                
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement", _userManager.Users);
                }

                ModelState.AddModelError("","User not updated,something wnt wrong");
                return View(user);
            }

            return RedirectToAction("UserManagement", _userManager.Users);
        }
    }
}
