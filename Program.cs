using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;
using ShopForHome.Api.Utils;   // ✅ Added this
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ShopContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=shopforhome.db"));

// builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopForHome API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Apply DB creation/seed
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopContext>();
    context.Database.Migrate();
    // ✅ Seed only if no products
    if (!context.Products.Any())
    {
        context.Products.AddRange(new List<Product> {
            new Product { Name="Modern Sofa", Category="Furniture", Price=29999, Rating=4.5M, ImageUrl="https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcS_nONCYlcaGsvvkSW4R9L1pCKU6BE91aKo5DgtA8Se_8_Lnu-1L8bJHLxapTItqM8kFRRemkoEpBXVgEuJ-BUHrnH1yDRgaoz91BXIsA5fslY9X3B08g0XGg&usqp=CAc", Stock=50 },
            new Product { Name="Table Lamp", Category="Lighting", Price=1499, Rating=4.2M, ImageUrl="https://th.bing.com/th/id/R.062de45143b0795f860e41fedce05821?rik=E36z8m6kffrUKQ&riu=http%3a%2f%2fhomedecorideas.uk%2fwp-content%2fuploads%2f2017%2f03%2fTable-Lamps-For-Living-Room-ceramic.jpg&ehk=WbNh6bLww9Lr%2fUw2mbEdezTk%2fwzRN2sEltJKLIyeqsY%3d&risl=&pid=ImgRaw&r=0", Stock=100 },
            new Product { Name="Wall Art", Category="Home Décor", Price=1999, Rating=4.0M, ImageUrl="https://th.bing.com/th/id/OIP.DyOYv0PWypaP8UCmFlIrKgHaHa?w=177&h=150&c=6&o=7&dpr=1.3&pid=1.7&rm=3", Stock=20 }
        });

        // ✅ Fixed: directly call PasswordHasher (not Utils.PasswordHasher)
        context.Users.Add(new User
        {
            Email = "admin@shopforhome.com",
            PasswordHash = PasswordHasher.Hash("Admin@123"),
            Role = "Admin",
            FullName = "Admin User"
        });

        context.SaveChanges();
    }
}

// Configure
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
// app.UseHttpsRedirection();  // <-- comment this line out

app.MapControllers();
app.Urls.Add("http://localhost:5000");

app.Run();
