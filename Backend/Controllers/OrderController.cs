using bageri2.api.ViewModels.Order;
using bageri2.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bageri.api.Data;

namespace bageri2.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Select(o => new
                    {
                        o.OrderNumber,
                        o.OrderDate,
                        Customer = new
                        {
                            o.Customer.CompanyName,
                            o.Customer.Phone
                        },
                        Products = o.OrderItems.Select(oi => new
                        {
                            oi.Product.Name,
                            oi.Quantity,
                            oi.Price,
                            TotalPrice = oi.Quantity * oi.Price
                        })
                    })
                    .ToListAsync();

                return Ok(new { success = true, data = orders });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte hämta beställningar"});
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Where(o => o.OrderId == id)
                    .Select(o => new
                    {
                        OrderNumber = o.OrderNumber,
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                        Customer = new
                        {
                            o.Customer.CompanyName,
                            o.Customer.Phone
                        },
                        Products = o.OrderItems.Select(oi => new
                        {
                            oi.Product.Name,
                            oi.Quantity,
                            oi.Price,
                            TotalPrice = oi.Quantity * oi.Price
                        })
                    })
                    .FirstOrDefaultAsync();

                if (order is null)
                {
                    return NotFound(new { success = false, message = "Beställningen hittades inte" });
                }

                return Ok(new { success = true, data = order });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte hämta beställningen"});
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddOrder(OrderPostViewModel model)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(model.CustomerId);

                if (customer is null)
                {
                    return NotFound(new { success = false, message = "Kunden finns inte" });
                }

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = model.OrderNumber,
                    CustomerId = model.CustomerId,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var item in model.OrderItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product is null)
                    {
                        return NotFound(new { success = false, message = $"Produkten med ID {item.ProductId} hittades inte" });
                    }

                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price
                    });
                }

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Beställningen skapad", data = order });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Kunde inte skapa beställningen" });
            }
        }
    }
}