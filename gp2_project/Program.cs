using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using gp2_project.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<gp2_projectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("gp2_projectContext") ?? throw new InvalidOperationException("Connection string 'gp2_projectContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
