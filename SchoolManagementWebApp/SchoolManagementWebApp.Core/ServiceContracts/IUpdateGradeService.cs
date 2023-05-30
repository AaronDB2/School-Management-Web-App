using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
	public interface IUpdateGradeService
	{
		/// <summary>
		/// Validates data and calls assignment repository for updating the assignment grade in data store
		/// </summary>
		/// <param name="updateGradeRequest">New grade for assignment</param>
		/// <returns>Updated assignment (AssignmentResponse)</returns>
		Task<AssignmentGradeResponse> UpdateAssignmentGrade(UpdateGradeRequest updateGradeRequest);
	}
}
