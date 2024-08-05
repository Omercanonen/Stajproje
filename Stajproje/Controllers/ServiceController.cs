using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stajproje.Models;

namespace Stajproje.Controllers
{
    //[Authorize]
    public class ServiceController : Controller
    {

        private readonly ServiceDbContext context;

        public ServiceController(ServiceDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Service(string sortOrder)
        {
            //var list = await context.Services.ToListAsync();
            ////var list = await context.Services.Include(m => m.UserId).OrderBy(m => m.Name).ToListAsync();
            //return View(list);
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["BrandSortParam"] = sortOrder == "brand" ? "brand_desc" : "brand";

            var services = from s in context.Services.Include(s => s.User)
                           select s;

            switch (sortOrder)
            {
                case "id_desc":
                    services = services.OrderByDescending(s => s.Id);
                    break;
                case "name":
                    services = services.OrderBy(s => s.User.Name);
                    break;
                case "name_desc":
                    services = services.OrderByDescending(s => s.User.Name);
                    break;
                case "brand":
                    services = services.OrderBy(s => s.Brand);
                    break;
                case "brand_desc":
                    services = services.OrderByDescending(s => s.Brand);
                    break;
                default:
                    services = services.OrderBy(s => s.Id);
                    break;
            }

            return View(await services.ToListAsync());
            var list = await context.Services
                            .Include(s => s.User) // User'ı dahil et
                            .OrderBy(s => s.User.Name) // Kullanıcı adlarına göre sırala
                            .ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> ServiceDetails()
        {

            return View();
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
            var users = await context.Users.ToListAsync(); // Kullanıcıları al
            ViewBag.Users = new SelectList(users, "UserId", "Name"); // Listeyi viewbag'e aktar

            var brands = await context.Services.Select(s => s.Brand).Distinct().ToListAsync();
            ViewBag.Brands = new SelectList(brands); 

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService([Bind("UserId,Brand,Model,SeriNo,Warranty,Fault,Procedures,PartsCost,ServiceCost,Description,DeliveryDate")] Service service, string NewBrand)
        {
            if (!string.IsNullOrEmpty(NewBrand))
            {
                service.Brand = NewBrand; // Yeni marka varsa onu kullan
            }
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
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin.");
                }
            }

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
