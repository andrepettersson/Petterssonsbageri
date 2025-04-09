using bageri.api.Data;
using bageri.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bageri.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;



    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        var suppliers = await _context.Suppliers
        .Select(supplier => new
        {
            supplier.SupplierId,
            supplier.FirstName,
            supplier.LastName,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone
        })
        .ToListAsync();

        return Ok(new { success = true, suppliers });

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        var supplier = await _context.Suppliers
        .Select(supplier => new
        {
            supplier.SupplierId,
            supplier.FirstName,
            supplier.LastName,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone
        })
        .SingleOrDefaultAsync(s => s.SupplierId == id);

        if (supplier == null)
        {
            return NotFound(new { success = false, message = $"Leverantören hittades inte" });
        }

        return Ok(new { success = true, supplier });
    }
    }

