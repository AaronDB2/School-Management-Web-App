using Microsoft.AspNetCore.Authorization;
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
builder.Services.AddScoped<IUpdateGradeService, UpdateGradeService>();
builder.Services.AddScoped<IEditCourseService, EditCourseService>();
builder.Services.AddScoped<IEditCourseMessageService, EditCourseMessageService>();
builder.Services.AddScoped<ICourseGetterService, CourseGetterService>();
builder.Services.AddScoped<IAssignmentGetterService, AssignmentGetterService>();
builder.Services.AddScoped<IFileService, FileService>();

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

// Configure authorization rules
builder.Services.AddAuthorization(options => 
{
	options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

// Url for login view when user is not authenticated
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/login";
});

var app = builder.Build();

// create application pipeline
app.UseStaticFiles(); // Middleware for serving static files
app.UseRouting(); // Middleware for routing
app.UseAuthentication(); // Middleware for reading authentication cookie
app.UseAuthorization(); // Middleware for authorization. Validates access permissions of the user
app.MapControllers(); // Middleware for Executing filter pipeline

app.Run();
