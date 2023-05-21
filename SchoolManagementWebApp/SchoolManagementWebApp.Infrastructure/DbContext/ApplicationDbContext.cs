using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using System.Reflection.Emit;

namespace SchoolManagementWebApp.Infrastructure.DbContext
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		/// <summary>
		/// Constructor. Configures the options parameter of the DbContext
		/// </summary>
		/// <param name="options">Options for DbContext</param>
		public ApplicationDbContext(DbContextOptions options) : base(options) { }
		public DbSet<Assignment> Assignments { get; set; }
		public DbSet<Course> Courses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Assignment>().ToTable("Assignments");
			builder.Entity<Course>().ToTable("Courses");

			foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}
	}
}
