using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO request for updating assignment grade
	/// </summary>
	public class UpdateGradeRequest
	{
		public int Grade { get; set; }

		public Guid AssignmentId { get; set; }

		public Guid CourseId { get; set; }
	}
}
