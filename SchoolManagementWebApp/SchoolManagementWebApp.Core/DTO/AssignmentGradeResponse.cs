using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO for responses related to assignment grade
	/// </summary>
	public class AssignmentGradeResponse
	{
		public Guid AssignmentId { get; set; }

		public int Grade { get; set; }
	}
}
