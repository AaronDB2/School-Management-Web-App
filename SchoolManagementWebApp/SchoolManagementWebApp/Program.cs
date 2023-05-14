var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); // Adds all controllers as services with views
var app = builder.Build();

// create application pipeline
app.UseStaticFiles(); // Middleware for serving static files
app.UseRouting(); // Middleware for routing
app.MapControllers(); // Middleware for enabling controller routing

app.Run();
