using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO for request to edit a course entity
	/// </summary>
	public class EditCourseRequest
	{
		public Guid CourseId { get; set; }

		public string CourseFileName { get; set; }

		public string CourseText { get;set; }
	}
}
