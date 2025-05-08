using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityChatEmail.Controllers
{
    public class MessageController : Controller
    {
        private readonly EmailContext _context; // Veritabanı işlemleri için kullanılacak context nesnesi
        private readonly UserManager<AppUser> _userManager; // Kullanıcı yönetimi için Identity sınıfı

        // Yapıcı metot (Constructor) - Controller oluşturulurken EmailContext ve UserManager dependency injection ile alınır
        public MessageController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize] // Bu action'a sadece giriş yapmış kullanıcılar erişebilir
        public async Task<IActionResult> Inbox()
        {
            // Aktif kullanıcı bilgilerini al
            var values1 = await _userManager.FindByNameAsync(User.Identity.Name);

            // ViewBag ile kullanıcı e-postasını view'a taşı
            ViewBag.email = values1.Email;
            ViewBag.v1 = values1.Name + " " + values1.Surname; // Kullanıcının ad ve soyadını birleştirerek View'a ViewBag üzerinden gönderir


            var values2 = _context.Messages.Where(x => x.ReceiverEmail == values1.Email).ToList(); // Giriş yapan kullanıcının e-posta adresine gelen tüm mesajları liste olarak getirir


            // Gelen kutusu sayfasını döndür
            return View(values2);
        }

        public async Task<IActionResult> SendBox()
        {
            // Giriş yapan kullanıcı bilgilerini kullanıcı adından (username) al
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            // Kullanıcının e-posta adresini al
            string emailValue = values.Email;

            // Veritabanından bu kullanıcı tarafından gönderilen mesajları filtrele
            var sendMessageList = _context.Messages.Where(x => x.SenderEmail == emailValue).ToList();

            // Gönderilen mesaj listesini View'a gönder
            return View(sendMessageList);
        }


        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string senderEmail = values.Email;

            message.SenderEmail = senderEmail;
            message.IsRead = false;
            message.SendDate = DateTime.Now;

            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("SendBox");
        }
    }
}
