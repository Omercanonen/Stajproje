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
                User.Title = viewModel.Title;
                User.TaxAdmin = viewModel.TaxAdmin;
                User.TaxNo = viewModel.TaxNo;

                context.Users.Update(User);
                await context.SaveChangesAsync();
            } 
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var user = await context.Users.FindAsync(Id);
            context.Remove(user);
            await context.SaveChangesAsync();
            return RedirectToAction("UserPage");

        }
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,PhoneNumber,RegStatus,Title,TaxAdmin,TaxNo")] User user)
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
