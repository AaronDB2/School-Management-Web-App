using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO for requesting to change course entity message
	/// </summary>
	public class EditCourseMessageRequest
	{
		public Guid CourseId { get; set; }

		public string CourseMessage { get; set; }
	}
}
