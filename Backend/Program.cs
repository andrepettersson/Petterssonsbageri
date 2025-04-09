using System.Text.Json.Serialization;
using bageri.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

builder.Services.AddDbContext<DataContext>(options =>
{
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion);
});
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var context = services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();
await Seed.LoadProducts(context);
await Seed.LoadSuppliers(context);
await Seed.LoadSuppliersProduct(context);
await Seed.LoadCustomers(context);
await Seed.LoadOrders(context);
await Seed.LoadOrderItems(context);

app.MapControllers();

app.Run();

