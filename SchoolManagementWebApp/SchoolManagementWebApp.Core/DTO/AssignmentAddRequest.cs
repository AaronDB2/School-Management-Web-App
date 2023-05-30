using SchoolManagementWebApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	public class AssignmentAddRequest
	{
		public string AssignmentFileName { get; set; }

		public Guid CourseId { get; set; }

		public Guid StudentId { get; set; }

		/// <summary>
		/// Converts DTO object to Assignment entity
		/// </summary>
		/// <returns>Assignment object with DTO data</returns>
		public Assignment ToAssignment()
		{
			return new Assignment() { AssignmentFileName = AssignmentFileName, CourseId = CourseId, StudentId = StudentId };
		}
	}
}
