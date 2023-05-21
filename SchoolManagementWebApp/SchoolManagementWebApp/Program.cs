using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Infrastructure.DbContext;

var builder = WebApplication.CreateBuilder(args);
// Adds all controllers as services with views
builder.Services.AddControllersWithViews();
// Add DbContext as a service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// create application pipeline
app.UseStaticFiles(); // Middleware for serving static files
app.UseRouting(); // Middleware for routing
app.MapControllers(); // Middleware for enabling controller routing

app.Run();
