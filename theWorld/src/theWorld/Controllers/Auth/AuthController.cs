using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace theWorld.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.CodeAnalysis.CSharp;

    using theWorld.Models;
    using theWorld.ViewModels;

    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                RedirectToAction("Trips", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vmViewModel, string ReturnUrl)
        {
            if (this.ModelState.IsValid)
            {
                var signInResults = await this._signInManager.PasswordSignInAsync(
                    vmViewModel.Username,
                    vmViewModel.Password,
                    true,
                    false);
                
                if (signInResults.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }                    
                }
                else
                {
                    this.ModelState.AddModelError("","Username or password incorrect ");
                }
            }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this._signInManager.SignOutAsync();                
            }
            return RedirectToAction("Index", "App");
        }
    }
}
