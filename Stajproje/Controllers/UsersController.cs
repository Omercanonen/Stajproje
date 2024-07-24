using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Stajproje.Models;

namespace Stajproje.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDbContext context;

        public UsersController(UserDbContext context) 
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var users = context.Users.ToList();
            return View("/Home/UserPage",users);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,PhoneNumber,RegStatus")] User user)
        {
            if(ModelState.IsValid)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
