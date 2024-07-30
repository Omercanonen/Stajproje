using Microsoft.AspNetCore.Mvc;
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
            var list = await context.Services.ToListAsync(); 
            return View(list);
        }
    }
}
