using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
    /// <summary>
	/// Represents logic for retrieving assignment data from data store
	/// </summary>
    public interface IAssignmentGetterService
    {
        /// <summary>
        /// Validates data and calls assignment repository to retrieve assignments for the given course id.
        /// </summary>
        /// <param name="courseId">Course id to search assignments for</param>
        /// <returns>List of all the assignments that match the given course id</returns>
        Task<List<AssignmentResponse>> GetAssignmentsByCourseId(Guid courseId);

		/// <summary>
		/// Gets the assignments that matches the search criteria
		/// </summary>
		/// <param name="searchBy">Value to search by</param>
		/// <param name="searchString">Search value</param>
		/// <returns>List of assignments as AssignmentResponse that matched the search criteria</returns>
		Task<List<AssignmentResponse>> GetFilterdAssignments(string searchBy, string? searchString);
	}
}
