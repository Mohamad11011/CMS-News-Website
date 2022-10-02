using Microsoft.AspNetCore.Identity;
using DgPadCMS.Models;
using Microsoft.EntityFrameworkCore;
using Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString),ServiceLifetime.Transient);

builder.Services.AddScoped<ActionServices>();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


builder.Services.ConfigureApplicationCookie(options =>
{
    //Location for your Custom Access Denied Page
    options.AccessDeniedPath = "/User/AccessDenied";
 
    //Location for your Custom Login Page
    options.LoginPath = "/User/Login";
    options.SlidingExpiration = true;
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

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Login}/{id?}");
});


app.Run();
