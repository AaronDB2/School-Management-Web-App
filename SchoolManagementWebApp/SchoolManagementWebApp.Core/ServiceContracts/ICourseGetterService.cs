using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
	/// <summary>
	/// Represents logic for retrieving course data from data store
	/// </summary>
	public interface ICourseGetterService
	{
		/// <summary>
		/// Gets a course from the data store that matches the course id
		/// </summary>
		/// <param name="coursId">Id of the course you want to retrieve from data store</param>
		/// <returns>Course from data store as CourseResponse</returns>
		Task<CourseResponse> GetCourseByCourseId(Guid coursId);

		/// <summary>
		/// Gets all the course entities from data store
		/// </summary>
		/// <returns>List of all the courses as RourseResponse</returns>
		Task<List<CourseResponse>> GetAllCourses();

		/// <summary>
		/// Gets the courses that matches the search criteria
		/// </summary>
		/// <param name="searchBy">Value to search by</param>
		/// <param name="searchString">Search value</param>
		/// <returns>List of courses as CourseResponse that matched the search criteria</returns>
		Task<List<CourseResponse>> GetFilterdCourses(string searchBy, string? searchString);
	}
}
