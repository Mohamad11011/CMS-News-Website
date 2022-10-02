using Common;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DgPadCMS.Controllers
{
        public class UserController : Controller
        {
            private readonly UserManager<AppUser> userManager;
            private readonly SignInManager<AppUser> signInManager;
            private IPasswordHasher<AppUser> passwordHasher;

            public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                                    IPasswordHasher<AppUser> passwordHasher)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.passwordHasher = passwordHasher;
            }

            //login
            [AllowAnonymous]
            public IActionResult Login(string returnUrl)
            {
                Login login = new Login
                {
                    ReturnUrl = returnUrl
                };

                return View(login);
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(Login login)
            {

                    AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                    if (appUser != null)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                        return RedirectToAction("Index", "Post", new { area = "Admin" });
                   
            }
                    ModelState.AddModelError("", "Login failed, wrong credentials.");

                return View(login);
            }
        //--*--*--//

        //logout
        public ActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }


            //Edit User
            public async Task<IActionResult> Edit()
            {
                AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);
                UserEdit user = new UserEdit(appUser);

                return View(user);
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(UserEdit user)
            {
                AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);

                if (ModelState.IsValid)
                {
                    appUser.Email = user.Email;
                    if (user.Password != null)
                    {
                        appUser.PasswordHash = passwordHasher.HashPassword(appUser, user.Password);
                    }

                    IdentityResult result = await userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                        TempData["Success"] = "Your information has been edited!";
                }

                return View();
            }

            //--*--*--//

            //Access Denied
            [HttpGet]
            [Route("/User/AccessDenied")]
            public ActionResult AccessDenied()
            {
                return View();
            }
            //--*--*--//
        }
}

