using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using PieShop.Auth;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model =  new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins =  (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Pie");
                }

            }

            ModelState.AddModelError("","User Name or Password is not correct");
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Register(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
               var user = new ApplicationUser()
               {
                   UserName = loginViewModel.UserName
               };

               var result = await _userManager.CreateAsync(user, loginViewModel.Password);

               if (result.Succeeded)
               {
                   return RedirectToAction("Index", "Pie");
               }
            }

            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Pie");
        }



        [HttpPost]
        [AllowAnonymous]
        public  IActionResult ExternalLogin(string provider,string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Account",
                new {returnUrl = returnUrl});

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider,properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null,string serviceError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (serviceError != null) 
            {
                ModelState.AddModelError("","Error from External Source");
                return View("Login",loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external provider info");
                return RedirectToAction("Login", loginViewModel);
            }

            var result = await _signInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false,true);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            var user = new ApplicationUser()
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Name).Value,
               
            };

            var identityResult = await _userManager.CreateAsync(user);

            if ( !identityResult.Succeeded)
                return new BadRequestResult();

            identityResult = await _userManager.AddLoginAsync(user, info);

            if (!identityResult.Succeeded)
                return new BadRequestResult();

            await _signInManager.SignInAsync(user, false);

            
            return LocalRedirect(returnUrl);

        }
    }
}
