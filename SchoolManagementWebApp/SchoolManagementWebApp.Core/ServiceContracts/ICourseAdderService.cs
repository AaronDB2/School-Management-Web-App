using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
	/// <summary>
	/// Represents business logic for inserting course entity
	/// </summary>
	public interface ICourseAdderService
	{
		/// <summary>
		/// Validates the course data and calls repository for adding courses to data store
		/// </summary>
		/// <param name="courseAddRequest">Course object to add</param>
		/// <returns>Returns the course object (CourseResponse) after adding it to the data store</returns>
		Task<CourseResponse> AddCourse(CourseAddRequest? courseAddRequest);
	}
}
