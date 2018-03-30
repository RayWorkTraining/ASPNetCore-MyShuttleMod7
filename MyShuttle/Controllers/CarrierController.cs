using Microsoft.AspNetCore.Authorization;
using MyShuttle.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyShuttle.Model;

namespace MyShuttle.Web.Controllers
{
    [Authorize]
    public class CarrierController : Controller
    {
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public CarrierController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //return RedirectToAction("Index", "Home");
                    var signInStatus = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (signInStatus.Succeeded)
                    {
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password."); return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("register", "Carrier");
                }
            }
            catch (System.Exception ex)
            {

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await SignInManager.SignOutAsync(); 

            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //return RedirectToAction("Login", "Carrier");

                    var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        return RedirectToAction("Register", "Carrier");
                    }
                }
                else
                {
                    return RedirectToAction("Register", "Carrier");
                }
            }
            catch (System.Exception ex)
            {

            }

            return RedirectToAction("Register", "Carrier");
        }
    }
}