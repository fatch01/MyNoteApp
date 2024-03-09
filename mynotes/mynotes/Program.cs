using Microsoft.EntityFrameworkCore;
using mynotes.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Session iþlemleri

// Session için gerekli olan Distributed Memory Cache servisini ekleyin
builder.Services.AddDistributedMemoryCache();

// Session ayarlarýný ekleyin
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Oturum zaman aþýmý süresi
    options.Cookie.HttpOnly = true; // Güvenlik için HTTP üzerinden eriþilebilir
    options.Cookie.IsEssential = true; // Cookie esastýr ve consent gerektirmez
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


// Session middleware'ýný ekleyin
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
