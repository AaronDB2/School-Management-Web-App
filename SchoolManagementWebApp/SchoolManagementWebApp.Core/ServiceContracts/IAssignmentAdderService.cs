using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
	/// <summary>
	/// Represents business logic for inserting assignment entity
	/// </summary>
	public interface IAssignmentAdderService
	{
		/// <summary>
		/// Validates the assignment data and calls repository for adding assignment to data store
		/// </summary>
		/// <param name="assignmentAddRequest">Assignment object to add</param>
		/// <returns>Returns the assignment object (AssignmentResponse) after adding it to the data store</returns>
		Task<AssignmentResponse> AddAssignment(AssignmentAddRequest? assignmentAddRequest);
	}
}
