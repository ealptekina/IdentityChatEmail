using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmail.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: /Login/UserLogin
        public IActionResult UserLogin()
        {
            return View();
        }

        // POST: /Login/UserLogin
        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName,model.Password,false,true);
            if (result.Succeeded)
            {
                return RedirectToAction("ProfileDetail", "Profile");
            }
            return View();
        }
    }
}
