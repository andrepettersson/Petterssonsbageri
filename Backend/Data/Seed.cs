
using System.Text.Json;
using bageri.api.Entities;
using bageri2.api.Entities;

namespace bageri.api.Data;

    public static class Seed
    {
        public static async Task LoadProducts(DataContext context)
        {
         var options = new JsonSerializerOptions
         {
            PropertyNameCaseInsensitive = true
         };

         if (context.Products.Any()) return;

         var json = File.ReadAllText("Data/json/products.json");
         var products = JsonSerializer.Deserialize<List<Product>>(json, options);

         if(products is not null && products.Count > 0)
         {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
         }
        }

        public static async Task LoadSuppliers(DataContext context)
        {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        if (context.Suppliers.Any()) return;

        var json = File.ReadAllText("Data/json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if(suppliers is not null && suppliers.Count > 0)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }
        }


        public static async Task LoadSuppliersProduct(DataContext context)
        {
            var options = new JsonSerializerOptions
        {
         PropertyNameCaseInsensitive = true        
        };
        if (context.SupplierProducts.Any()) return;

        var json = File.ReadAllText("Data/json/supplierproducts.json");
        var supplierproducts = JsonSerializer.Deserialize<List<SupplierProduct>>(json, options);

        if(supplierproducts is not null && supplierproducts.Count >0)
        {
            await context.SupplierProducts.AddRangeAsync(supplierproducts);
            await context.SaveChangesAsync();
        }
        }


        public static async Task LoadCustomers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

           
            if (context.Customers.Any()) return;

            var json = File.ReadAllText("Data/json/customer.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(json, options);

            if (customers is not null && customers.Count > 0)
            {
                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadOrders(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

       
            if (context.Orders.Any()) return;

            var json = File.ReadAllText("Data/json/order.json");
            var orders = JsonSerializer.Deserialize<List<Order>>(json, options);

            if (orders is not null && orders.Count > 0)
            {
                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadOrderItems(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.OrderItems.Any()) return;

            var json = File.ReadAllText("Data/json/orderitems.json");
            var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(json, options);

            if (orderItems is not null && orderItems.Count > 0)
            {
                await context.OrderItems.AddRangeAsync(orderItems);
                await context.SaveChangesAsync();
            }
        }



        

    }
    
    
    