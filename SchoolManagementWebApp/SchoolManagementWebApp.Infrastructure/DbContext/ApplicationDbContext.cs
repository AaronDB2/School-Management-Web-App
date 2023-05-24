using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;

namespace SchoolManagementWebApp.Infrastructure.DbContext
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		/// <summary>
		/// Constructor. Configures the options parameter of the DbContext
		/// </summary>
		/// <param name="options">Options for DbContext</param>
		public ApplicationDbContext(DbContextOptions options) : base(options) 
		{
		}

		// Db sets
		public DbSet<Assignment> Assignments { get; set; }
		public DbSet<Course> Courses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Assignment>().ToTable("Assignments");
			builder.Entity<Course>().ToTable("Courses");


			//builder.Entity<IdentityUserRole<string>>().HasNoKey();
			// Disables cascade delete in database for all relations
			foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			// Seed role data
			builder.Entity<ApplicationRole>().HasData(new ApplicationRole
			{
				Name = "Admin",
				NormalizedName = "ADMIN",
				Id = new Guid("DF2B31EF-1940-40B1-976D-9A251B84512D"),
				ConcurrencyStamp = "1"
			}, new ApplicationRole
			{
				Name = "Teacher",
				NormalizedName = "TEACHER",
				Id = new Guid("6C4CB79B-4863-4D1B-BFBD-A2C9B78543E7"),
				ConcurrencyStamp = "2"
			}, new ApplicationRole
			{
				Name = "Student",
				NormalizedName = "STUDENT",
				Id = new Guid("09A8113E-55DD-422E-BEE0-8CEF1AF547E2"),
				ConcurrencyStamp = "3"
			});

			//Seed user data
			ApplicationUser user = new ApplicationUser()
			{
				Id = new Guid("8C66808C-5A90-47FD-8D25-BBE3F5AC1985"),
				UserName = "Admin",
				Email = "admin@gmail.com",
				LockoutEnabled = false,
			};
			ApplicationUser user2 = new ApplicationUser()
			{
				Id = new Guid("6E0CBCD5-3807-4813-95D1-930B9A220F27"),
				UserName = "Teacher",
				Email = "teacher@gmail.com",
				LockoutEnabled = false,
			};
			ApplicationUser user3 = new ApplicationUser()
			{
				Id = new Guid("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
				UserName = "Student",
				Email = "student@gmail.com",
				LockoutEnabled = false,
			};

			PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
			user.PasswordHash = passwordHasher.HashPassword(user, "Admin123");
			PasswordHasher<ApplicationUser> passwordHasher2 = new PasswordHasher<ApplicationUser>();
			user2.PasswordHash = passwordHasher.HashPassword(user, "Teacher123");
			PasswordHasher<ApplicationUser> passwordHasher3 = new PasswordHasher<ApplicationUser>();
			user3.PasswordHash = passwordHasher.HashPassword(user, "Student123");

			builder.Entity<ApplicationUser>().HasData(user);
			builder.Entity<ApplicationUser>().HasData(user2);
			builder.Entity<ApplicationUser>().HasData(user3);

			// Seed user role join table data
			builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
			{
				RoleId = Guid.Parse("DF2B31EF-1940-40B1-976D-9A251B84512D"),
				UserId = Guid.Parse("8C66808C-5A90-47FD-8D25-BBE3F5AC1985")
			}, new IdentityUserRole<Guid>
			{
				RoleId = Guid.Parse("6C4CB79B-4863-4D1B-BFBD-A2C9B78543E7"),
				UserId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27")
			}, new IdentityUserRole<Guid>
			{
				RoleId = Guid.Parse("09A8113E-55DD-422E-BEE0-8CEF1AF547E2"),
				UserId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C")
			});

			// Seed course data
			builder.Entity<Course>().HasData(new Course
			{
				CourseId = new Guid("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				CourseName = "Test Course",
				Message = "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"",
				CourseText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
				CourseFileName = "Test.pdf",
				TeacherId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27")
			});

			// Seed assignment
			builder.Entity<Assignment>().HasData(new Assignment
			{
				AssignmentID = new Guid("DA641EE7-004A-4543-8402-E5E897349FF5"),
				AssignmentFileName = "TestAssignment.pdf",
				CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				StudentId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
			});

			// Seed course student join table data
			builder.Entity("ApplicationUserCourse").HasData(new
			{
				CoursesCourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				StudentsId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
			});
		}
	}
}
