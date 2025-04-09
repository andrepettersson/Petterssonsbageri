using bageri.api.Data;
using bageri.api.Entities;
using bageri.api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace bageri.api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SupplierProductController(DataContext context) : ControllerBase
{
 private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllSupplierProducts()
    {
     var suppliersproducts = await _context.SupplierProducts
    .Include(sp => sp.Product)
    .Include(sp => sp.Supplier)
    .Select(sp => new SupplierProductViewModel
     {
       SupplierId = sp.SupplierId,
       ProductId = sp.ProductId,
       ProductName = sp.Product.Name,
       ItemNumber = sp.Product.ItemNumber,
       Quantity = sp.Quantity,
       PricePerKg = sp.PricePerKg,
     })
     .ToListAsync();

     return Ok(new{success = true, suppliersproducts});
     }
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProductAndSupplier(int productid)
    {
        var SuppliersAndProducts = await _context.SupplierProducts
        .Where(sp => sp.ProductId == productid)
        .Include(sp => sp.Product)
        .Include(sp => sp.Supplier)
        .Select(sp => new
        {
            sp.SupplierId,
            sp.ProductId,
            sp.ProductName,
            sp.ItemNumber,
            sp.Quantity,
            sp.PricePerKg,
            sp.Product.Name,
            sp.Supplier.FirstName,
            sp.Supplier.LastName
        })
        .ToListAsync();

        if(SuppliersAndProducts == null)
        {
            return NotFound(new {success = false, message = $"Produkten/Leverantören hittades inte"});
        }
        return Ok(new {success = true, SuppliersAndProducts});
    }

    [HttpPost]
    public async Task<ActionResult> AddProductToSupplier(SupplierProductViewModel model)
    {
    var supplier = await _context.Suppliers.SingleOrDefaultAsync(s => s.SupplierId == model.SupplierId);
    var product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == model.ProductId);

    if(supplier == null)
    {
        return NotFound(new {success = false, message = $"Leverantören/Produkten hittades inte"});
    }

    var supplierProduct = new SupplierProduct
    {
        SupplierId = model.SupplierId,
        ProductId = model.ProductId,
        ProductName = product.Name,
        Quantity = model.Quantity,
        PricePerKg = model.PricePerKg
    };

    try
    {
        await _context .SupplierProducts.AddAsync(supplierProduct);
        await _context.SaveChangesAsync();

        return Ok(new {success = true, message = $"Produkten är tillagd till Leverantören"});
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
    }
    
    

}

