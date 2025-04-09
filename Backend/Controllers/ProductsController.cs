using bageri2.api.ViewModels.Product;
using bageri2.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bageri.api.ViewModels;
using bageri.api.Data;
using bageri.api.Entities;

namespace bageri2.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                var products = await _context.Products
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        p.Weight,
                        p.PackSize,
                        p.ExpiryDate,
                        p.ManufacturerDate
                    })
                    .ToListAsync();

                return Ok(new { success = true, data = products });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte hämta produkter" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products
                    .Where(p => p.ProductId == id)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        p.Weight,
                        p.PackSize,
                        ExpiryDate = p.ExpiryDate.ToString("yyyy-MM-dd"),
                        ManufacturerDate = p.ManufacturerDate.ToString("yyyy-MM-dd")
                    })
                    .ToListAsync();

                if (product is null)
                {
                    return NotFound(new { success = false, message = "Produkten hittades inte" });
                }

                return Ok(new { success = true, data = product });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte hämta produkten" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductPostViewModel model)
        {
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Weight = model.Weight,
                    PackSize = model.PackSize,
                    ExpiryDate = model.ExpiryDate,
                    ManufacturerDate = model.ManufacturerDate
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, data = product });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte lägga till produkten" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrice(int id, ProductPutViewModel model)
        {
            try
            {
                var product = await _context.Products
                    .Where(p => p.ProductId == id)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new { success = false, message = "Produkten hittades inte" });
                }

                product.Price = model.Price;

                await _context.SaveChangesAsync();

                return Ok(new { success = true, data = product });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte uppdatera priset på produkten" });
            }
        }

    }
}