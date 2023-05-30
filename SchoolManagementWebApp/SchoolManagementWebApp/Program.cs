using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.Core.Services;
using SchoolManagementWebApp.Infrastructure.DbContext;
using SchoolManagementWebApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Adds all controllers as services with views
builder.Services.AddControllersWithViews();

// Add services
builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();

builder.Services.AddScoped<ICourseAdderService, CourseAdderService>();
builder.Services.AddScoped<IAssignmentAdderService, AssignmentAdderService>();

// Add DbContext as a service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Configure Identity
builder.Services
	.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders()
	.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
	.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

var app = builder.Build();

// create application pipeline
app.UseStaticFiles(); // Middleware for serving static files
app.UseRouting(); // Middleware for routing
app.MapControllers(); // Middleware for enabling controller routing

app.Run();
