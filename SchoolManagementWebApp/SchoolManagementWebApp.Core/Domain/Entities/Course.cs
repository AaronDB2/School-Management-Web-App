using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementWebApp.Core.Domain.Entities
{
	/// <summary>
	/// Model for Course entity for database
	/// </summary>
	public class Course
	{
		[Key]
		public Guid CourseId { get; set; }

		[StringLength(100)]
		public string CourseName { get; set; }

		[StringLength(500)]
		public string? Message { get; set; }

		[StringLength(1000)]
		public string? CourseText { get; set; }

		[StringLength(100)]
		public string? CourseFileName { get; set; }

		//Needed for creating foreign key in Assignment entity
		public ICollection<Assignment>? Assignments { get; set; }

		//Creates foreign key to ApplicationUser entity primary key
		public Guid TeacherId { get; set; }
		public ApplicationUser Teacher { get; set; }

		//Needed for many to many relationship with ApplicationUser entity
		public List<ApplicationUser> Students { get; } = new();
	}
}
