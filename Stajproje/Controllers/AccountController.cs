using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stajproje.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Stajproje.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(AccountContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Accounts.FirstOrDefault(a => a.Username == model.Name && a.Password == model.Password);

                if (account != null)
                {
                    var user = new IdentityUser { UserName = account.Username };
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(model.ReturnUrl ?? "/Home/Index");
                }
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            }
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }
    }
}



