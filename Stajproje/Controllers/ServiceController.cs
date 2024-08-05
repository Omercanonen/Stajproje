using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stajproje.Models;

namespace Stajproje.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ServiceDbContext context;

        public ServiceController(ServiceDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Service()
        {
            //var list = await context.Services.ToListAsync();
            ////var list = await context.Services.Include(m => m.UserId).OrderBy(m => m.Name).ToListAsync();
            //return View(list);
            var list = await context.Services
                            .Include(s => s.User) // User'ı dahil et
                            .OrderBy(s => s.User.Name) // Kullanıcı adlarına göre sırala
                            .ToListAsync();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> EditService(int Id)
        {
            var Service = await context.Services.FindAsync(Id);

            return View(Service);
        }
        [HttpPost]
        public async Task<IActionResult> EditService(Service viewModel)
        {
            var Service = await context.Services.FindAsync(viewModel.Id);

            if (Service != null)
            {
                Service.UserId = viewModel.UserId;
                Service.Brand = viewModel.Brand;
                Service.Model = viewModel.Model;
                Service.SeriNo = viewModel.SeriNo;
                Service.Warranty = viewModel.Warranty;
                Service.Fault = viewModel.Fault;
                Service.Procedures = viewModel.Procedures;
                Service.PartsCost = viewModel.PartsCost;
                Service.ServiceCost = viewModel.ServiceCost;
                Service.Description = viewModel.Description;
                Service.DeliveryDate = viewModel.DeliveryDate;

                context.Services.Update(Service);
                await context.SaveChangesAsync();
            }
            return View(viewModel);
        }
        public async Task<IActionResult> DeleteService(int Id)
        {
            var service = await context.Services.FindAsync(Id);
            context.Remove(service);
            await context.SaveChangesAsync();
            return RedirectToAction("Service");

        }
        public async Task<IActionResult> CreateService()
        {
            var users = await context.Users.ToListAsync(); // Tüm kullanıcıları al
            ViewBag.Users = new SelectList(users, "UserId", "UserId"); // Kullanıcı listesini ViewBag'e aktar

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService([Bind("UserId,Brand,Model,SeriNo,Warranty,Fault,Procedures,PartsCost,ServiceCost,Description,DeliveryDate")] Service service)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Services.Add(service);
            //    await context.SaveChangesAsync();
            //    return RedirectToAction("CreateService");
            //}
            //return View(service);
            if (ModelState.IsValid)
            {
                try
                {
                    context.Services.Add(service);
                    await context.SaveChangesAsync();
                    return RedirectToAction("CreateService");
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
            //var users = await context.Users.ToListAsync();
            //ViewBag.Users = new SelectList(users, "UserId", "Name");

            return View(service);

        }
    }
}
