using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmail.Controllers
{
    public class RegisterController : Controller
    {
        // Identity kullanıcı yöneticisi bağımlılığı (AppUser tipi ile)
        private readonly UserManager<AppUser> _userManager;

        // Constructor (bağımlılık enjeksiyonu ile UserManager nesnesi alınır)
        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // Kayıt sayfası görüntülendiğinde çalışacak GET metodu
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(); // Boş formu döndürür
        }

        // Kayıt formu gönderildiğinde çalışacak POST metodu
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            // ViewModel'den gelen verilerle yeni kullanıcı nesnesi oluşturuluyor
            AppUser appUser = new AppUser()
            {
                Name = model.Name,               // Adı
                Surname = model.Surname,         // Soyadı
                Email = model.Email,             // E-posta
                UserName = model.UserName        // Kullanıcı adı
            };

            // Identity sistemi üzerinden kullanıcı oluşturma işlemi yapılır.Password burada hashli gönderilir.
            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                // Kayıt başarılıysa, login sayfasına yönlendirilir
                return RedirectToAction("UserLogin", "Login");
            }
            else
            {
                // Kayıt başarısızsa, hata mesajları eklenir ve form tekrar gösterilir
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
        }
    }
}
