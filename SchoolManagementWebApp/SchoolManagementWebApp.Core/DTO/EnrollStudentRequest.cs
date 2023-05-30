using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO object for requesting to enroll a student into a course 
	/// </summary>
	public class EnrollStudentRequest
	{
		public Guid CourseId { get; set; }

		public Guid StudentId { get; set; }
	}
}
