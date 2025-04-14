using System.Text.Json.Serialization;
using bageri.api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(9, 0, 0));

builder.Services.AddDbContext<DataContext>(options =>
{
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseCors("AllowAll");

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

