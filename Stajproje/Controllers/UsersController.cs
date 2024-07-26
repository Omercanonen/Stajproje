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

        [HttpGet]
        public async Task<ActionResult> UserPage()
        {
            var list = await context.Users.ToListAsync();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var User = await context.Users.FindAsync(Id);

            return View(User);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User viewModel)
        {
           var User = await context.Users.FindAsync(viewModel.UserId);

            if (User != null) 
            {
                User.Name = viewModel.Name;
                User.Surname = viewModel.Surname;
                User.Email = viewModel.Email;
                User.PhoneNumber = viewModel.PhoneNumber;
                User.RegStatus = viewModel.RegStatus;

                await context.SaveChangesAsync();
            }
            return RedirectToAction("UserPage","UserController");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var user = await context.Users.FindAsync(Id);
            context.Remove(user);
            await context.SaveChangesAsync();
            return RedirectToAction("UserPage");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,PhoneNumber,RegStatus")] User user)
        {
            if (ModelState.IsValid)
            {

                context.Users.Add(user);
                await context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            return View(user);
        }
       
    }
}
