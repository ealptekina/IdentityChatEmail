using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmail.Controllers
{
    public class ProfileController : Controller
    {
        private readonly EmailContext _context; // Veritabanı işlemleri için context
        private readonly UserManager<AppUser> _userManager; // Identity kullanıcı işlemleri için UserManager

        // Constructor - bağımlılıkları enjekte ediyor
        public ProfileController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Kullanıcının profil bilgilerini görüntülemek için kullanılan aksiyon metodu
        public async Task<IActionResult> ProfileDetail()
        {
            // Giriş yapan kullanıcının bilgilerini username'e göre bul
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            // ViewBag üzerinden kullanıcı bilgilerini View'a gönder
            ViewBag.name = values.Name;
            ViewBag.surname = values.Surname;
            ViewBag.email = values.Email;
            ViewBag.phone = values.PhoneNumber;

            // Profil detaylarını gösterecek view'ı döndür
            return View();
        }
    }
}
