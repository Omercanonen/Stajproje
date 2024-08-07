using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stajproje.Models;

namespace Stajproje.Controllers
{
    //[Authorize]
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly ServiceDbContext context;

        public ServiceController(ServiceDbContext context)
        {
            this.context = context;
            _logger = _logger;
        }
        [HttpGet]
        public async Task<IActionResult> Service(string sortOrder)
        {
 
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["BrandSortParam"] = sortOrder == "brand" ? "brand_desc" : "brand";

            var services = from s in context.Services.Include(s => s.User)
                           select s;

            switch (sortOrder)
            {
                case "id_desc":
                    services = services.OrderByDescending(s => s.ServiceId);
                    break;
                case "name":
                    services = services.OrderBy(s => s.User.Name);
                    break;
                case "name_desc":
                    services = services.OrderByDescending(s => s.User.Name);
                    break;
                case "brand":
                    services = services.OrderBy(s => s.BrandId);
                    break;
                case "brand_desc":
                    services = services.OrderByDescending(s => s.BrandId);
                    break;
                default:
                    services = services.OrderBy(s => s.ServiceId);
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
            var Service = await context.Services.FindAsync(viewModel.ServiceId);

            if (Service != null)
            {
                Service.UserId = viewModel.UserId;
                Service.BrandId = viewModel.BrandId;
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
       

        [HttpGet]
        public async Task<IActionResult> CreateService()
        {
            var users = await context.Users.ToListAsync(); // Kullanıcıları al
            ViewBag.Users = new SelectList(users, "UserId", "Name"); // Listeyi viewbag'e aktar

            //var brands = await context.Brands.ToListAsync();
            //ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName");
            ViewBag.Brands = new SelectList(context.Brands, "BrandId", "BrandName");
            
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService([Bind("UserId,BrandId,Model,SeriNo,Warranty,Fault,Procedures,PartsCost,ServiceCost,Description,DeliveryDate")] Service service, string NewBrand)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(NewBrand))
                    {
                        var brand = new Brand { BrandName = NewBrand };
                        context.Brands.Add(brand);
                        await context.SaveChangesAsync();
                        service.BrandId = brand.BrandId;
                    }

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

            var users = await context.Users.ToListAsync();
            ViewBag.Users = new SelectList(users, "UserId", "Name");

            var brands = await context.Brands.ToListAsync();
            ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName");

            return View(service);

        }
    


}
    }











