using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementWebApp.Core.Domain.Entities
{
	/// <summary>
	/// Model for Assignment entity for database
	/// </summary>
	public class Assignment
	{
		[Key]
		public Guid AssignmentID { get; set; }

		[DefaultValue(0)]
		public int Grade { get; set; }

		[StringLength(100)]
		public string? AssignmentFileName { get; set; }

		//Creates foreign key to Course entity primary key
		// Do i need this?: public Guid CourseId { get; set; }
		public Course Course { get; set;}

		//Creates foreign key to ApplicationUser entity primary key
		public ApplicationUser Student { get; set; }
	}
}
