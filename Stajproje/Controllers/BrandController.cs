using Microsoft.AspNetCore.Mvc;
using Stajproje.Models;

namespace Stajproje.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandDbContext _context;

        public BrandController (BrandDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Brands.Add(brand);
                    await _context.SaveChangesAsync();
                    return Json(new { BrandId = brand.BrandId, BrandName = brand.BrandName });
                }
                catch (Exception ex)
                {
                    return BadRequest("Veritabanına kaydedilirken bir hata oluştu: " + ex.Message);
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest("Geçersiz veri: " + string.Join(", ", errors));
        }

    }
}
