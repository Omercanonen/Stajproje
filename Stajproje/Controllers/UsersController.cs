using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }
        public IActionResult UserPage()
        {
            var list = context.Users.ToList();
            return View(list);
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
                return RedirectToAction("Create");
            }
            return View(user);
        }
       
    }
}
