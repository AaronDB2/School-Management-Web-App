using Microsoft.AspNetCore.Identity;
using SchoolManagementWebApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementWebApp.Core.Domain.IdentityEntities
{
	/// <summary>
	/// Model for ApplicationUser entity for database
	/// </summary>
	public class ApplicationUser : IdentityUser<Guid>
	{
		//Needed for creating foreign key in Assignment entity
		public ICollection<Assignment> Assignments { get; set; }

		//Needed for creating foreign key in Course entity
		[InverseProperty("Teacher")]
		public ICollection<Course> CoursesTeached { get; set; }

		//Needed for many to many relationship with Course entity
		[InverseProperty("Students")]
		public List<Course> Courses { get; set; } = new();
	}
}
