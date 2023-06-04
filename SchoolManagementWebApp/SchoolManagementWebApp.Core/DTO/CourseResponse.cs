using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO object for responses that involve Course entity
	/// </summary>
	public class CourseResponse
	{
		public Guid CourseId { get; set; }

		public string CourseName { get;set; }

		public string CourseText { get; set; }

		public string CourseMessage { get; set; }

		public string CourseFileName { get; set; }

		public ICollection<Assignment>? Assignments { get; set; }

		public Guid TeacherId { get; set; }

		public List<ApplicationUser> Students { get; set; }
	}
}
