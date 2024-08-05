using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Stajproje.Models;

namespace Stajproje.Controllers
{

    //[Authorize]
    public class UsersController : Controller
    {
        private readonly UserDbContext context;

        public UsersController(UserDbContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> UserPage(string sortOrder)
        {
            //var list = await context.Users.ToListAsync();
            //return View(list);
            ViewData["UserIdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "UserId_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["SurnameSortParam"] = sortOrder == "Surname" ? "Surname_desc" : "Surname";
            ViewData["EmailSortParam"] = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewData["PhoneNumberSortParam"] = sortOrder == "PhoneNumber" ? "PhoneNumber_desc" : "PhoneNumber";

            // Kullanıcıları veritabanından alın
            var users = from s in context.Users
                        select s;

            // Sıralama düzenini belirleyin
            switch (sortOrder)
            {
                case "UserId_desc":
                    users = users.OrderByDescending(s => s.UserId);
                    break;
                case "Name":
                    users = users.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case "Surname":
                    users = users.OrderBy(s => s.Surname);
                    break;
                case "Surname_desc":
                    users = users.OrderByDescending(s => s.Surname);
                    break;
                case "Email":
                    users = users.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    users = users.OrderByDescending(s => s.Email);
                    break;
                case "PhoneNumber":
                    users = users.OrderBy(s => s.PhoneNumber);
                    break;
                case "PhoneNumber_desc":
                    users = users.OrderByDescending(s => s.PhoneNumber);
                    break;
                default:
                    users = users.OrderBy(s => s.UserId);
                    break;
            }

            // Sonuçları view'a gönderin
            return View(await users.ToListAsync());

            // Sonuçları view'a gönderin
            return View(await users.ToListAsync());
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
            //if (ModelState.IsValid)
            //{

            //    context.Users.Add(user);
            //    await context.SaveChangesAsync();
            //    return RedirectToAction("Create");
            //}
            //return View(user);
            if (ModelState.IsValid)
            {
                try
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    // Hata mesajını loglayın
                    Console.WriteLine(ex.Message);
                    // Kullanıcıya uygun bir mesaj gösterin
                    ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin.");
                }
            }

            // Hataları kontrol et ve logla
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(user);

        }


    }
}
