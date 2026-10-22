using Microsoft.EntityFrameworkCore;
using WoodCraftFurniture.Data;
using WoodCraftFurniture.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=WoodCraftDb.db";
var useSqlite = connectionString.Contains("Data Source", StringComparison.OrdinalIgnoreCase);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
if (useSqlite)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString,
            new MySqlServerVersion(new Version(8, 0, 21))));
}

// Add session for cart
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    SeedData(context);
}

app.Run();

static void SeedData(ApplicationDbContext context)
{
    if (context.Products.Any())
    {
        return;
    }

    context.Products.AddRange(
        new Product
        {
            Name = "Milano 3-Seater Sofa",
            Category = "Sofas",
            Price = 899.99m,
            Description = "A modern three-seater sofa with solid oak legs and high-density foam cushions.",
            ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=500",
            IsFeatured = true
        },
        new Product
        {
            Name = "Linen 2-Seater Loveseat",
            Category = "Sofas",
            Price = 549.00m,
            Description = "Compact linen loveseat, perfect for apartments and small living rooms.",
            ImageUrl = "https://images.unsplash.com/photo-1540574163026-643ea20ade25?w=500"
        },
        new Product
        {
            Name = "Oakland King Bed Frame",
            Category = "Beds",
            Price = 1249.00m,
            Description = "Solid wood king bed frame with slatted base and headboard.",
            ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?w=500",
            IsFeatured = true
        },
        new Product
        {
            Name = "Canopy Queen Bed",
            Category = "Beds",
            Price = 1099.50m,
            Description = "Elegant canopy bed with matte black metal frame and fabric drapes.",
            ImageUrl = "https://images.unsplash.com/photo-1616594039964-ae9021a400a0?w=500"
        },
        new Product
        {
            Name = "Rustic 6-Seat Dining Table",
            Category = "Tables",
            Price = 789.00m,
            Description = "Reclaimed wood dining table seating six, with a natural matte finish.",
            ImageUrl = "https://images.unsplash.com/photo-1617806118233-18e1de247200?w=500",
            IsFeatured = true
        },
        new Product
        {
            Name = "Marble-Top Coffee Table",
            Category = "Tables",
            Price = 349.99m,
            Description = "Round coffee table with a marble top and matte black metal base.",
            ImageUrl = "https://images.unsplash.com/photo-1532372320572-cda25653a26d?w=500"
        },
        new Product
        {
            Name = "Copenhagen Dining Chair",
            Category = "Chairs",
            Price = 129.00m,
            Description = "Scandinavian dining chair with a woven cord seat and ash frame.",
            ImageUrl = "https://images.unsplash.com/photo-1503602642458-232111445657?w=500",
            IsFeatured = true
        },
        new Product
        {
            Name = "Ergonomic Office Chair",
            Category = "Chairs",
            Price = 249.99m,
            Description = "Mesh-back office chair with adjustable lumbar support and armrests.",
            ImageUrl = "https://images.unsplash.com/photo-1580480055273-228ff5388ef8?w=500"
        },
        new Product
        {
            Name = "Nordic 3-Door Wardrobe",
            Category = "Wardrobes",
            Price = 1450.00m,
            Description = "Three-door wardrobe in light oak with hanging rail, shelves and drawers.",
            ImageUrl = "https://images.unsplash.com/photo-1616486338812-3dadae4b4ace?w=500",
            IsFeatured = true
        },
        new Product
        {
            Name = "2-Drawer Bedside Table",
            Category = "Wardrobes",
            Price = 159.00m,
            Description = "Compact bedside cabinet with two soft-close drawers.",
            ImageUrl = "https://images.unsplash.com/photo-1532323544230-7191fd51bc1b?w=500"
        }
    );

    context.SaveChanges();
}
