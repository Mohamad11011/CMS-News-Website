using Common;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DgPadCMS.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class UserManageController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;
        public UserManageController(RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.signInManager = signInManager;
        }

        //View Users Available
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
        //--*--*--//


        //Add New User(s)
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(user);
        }
        //--*--*--//
        [Authorize(Roles = "Admin")]
        // Delete
        public async Task<IActionResult> Delete(string id)
        {

            var user = await userManager.FindByIdAsync(id);

            await userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        //**--**--**-

        //LogOut
        
        public ActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login", "User",new { area = "" });
        }


    }

}
