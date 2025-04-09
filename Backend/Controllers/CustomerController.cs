using bageri2.api.ViewModels.Customer;
using bageri2.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bageri.api.Data;

namespace bageri2.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _context.Customers
                    .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Select(c => new
                    {
                        c.CustomerId,
                        c.CompanyName,
                        c.Phone,
                        c.Email,
                        c.ContactPerson,
                        c.DeliveryAddress,
                        c.InvoiceAddress,
                        Orders = c.Orders.Select(o => new
                        {
                            o.OrderId,
                            o.OrderDate,
                            o.OrderNumber,
                            OrderItems = o.OrderItems.Select(oi => new
                            {
                                oi.OrderItemId,
                                oi.Quantity,
                                oi.Price,
                                TotalPrice = oi.Quantity * oi.Price,
                                Product = new
                                {
                                    oi.Product.Name,
                                    oi.Product.Price
                                }
                            }).ToList()
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(new { success = true, data = customers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte hämta kunder", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.Customers
                    .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Where(c => c.CustomerId == id)
                    .Select(c => new
                    {
                        c.CustomerId,
                        c.CompanyName,
                        c.Phone,
                        c.Email,
                        c.ContactPerson,
                        c.DeliveryAddress,
                        c.InvoiceAddress,
                        Orders = c.Orders.Select(o => new
                        {
                            o.OrderId,
                            o.OrderDate,
                            o.OrderNumber,
                            Customer = new
                            {
                                o.Customer.CustomerId,
                                o.Customer.CompanyName,
                                o.Customer.Phone
                            },
                            OrderItems = o.OrderItems.Select(oi => new
                            {
                                oi.OrderItemId,
                                oi.Quantity,
                                oi.Price,
                                TotalPrice = oi.Quantity * oi.Price,
                                Product = new
                                {
                                    oi.Product.Name,
                                    oi.Product.Price
                                }
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (customer is null)
                {
                    return NotFound(new { success = false, message = "Kunden hittades inte" });
                }

                return Ok(new { success = true, data = customer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte hämta kunden", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(CustomerPostViewModel model)
        {
            try
            {
                var customer = new Customer
                {
                    CompanyName = model.CompanyName,
                    Phone = model.Phone,
                    Email = model.Email,
                    ContactPerson = model.ContactPerson,
                    DeliveryAddress = model.DeliveryAddress,
                    InvoiceAddress = model.InvoiceAddress
                };

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { success = true, message = "Kunden skapad", data = customer });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte skapa kunden" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, CustomerPutViewModel model)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return NotFound(new { success = false, message = "Kunden finns inte" });
                }

                customer.ContactPerson = model.ContactPerson;
                customer.DeliveryAddress = model.DeliveryAddress;
                customer.InvoiceAddress = model.InvoiceAddress;

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Kunden uppdaterad", data = customer });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte uppdatera kunden" });
            }
        }
    }
}