using Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ActionServices>();
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

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
        name: "Details",
        pattern: "{controller=Post}/{action=Details}/{id}"
    );
   

    endpoints.MapControllerRoute(
         name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}"
    );
});


app.Run();
