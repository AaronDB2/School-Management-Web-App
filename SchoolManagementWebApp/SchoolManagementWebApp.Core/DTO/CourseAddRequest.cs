using SchoolManagementWebApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO for adding course to data store
	/// </summary>
	public class CourseAddRequest
	{
		public string CourseName { get; set; }

		public Guid TeacherId { get; set; }

		/// <summary>
		/// Converts DTO object to Course entity
		/// </summary>
		/// <returns>Course object with DTO data</returns>
		public Course ToCourse()
		{
			return new Course() { CourseName = CourseName, TeacherId = TeacherId };
		}
	}
}
