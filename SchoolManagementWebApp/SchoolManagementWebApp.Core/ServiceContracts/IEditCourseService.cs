using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
	public interface IEditCourseService
	{
		/// <summary>
		/// Validates request data and calls repository for updating course entity.
		/// Will only update courseText and courseFileName
		/// </summary>
		/// <param name="editCourseRequest">Course that will be updated</param>
		/// <returns>Course that has been updated (CourseResponse)</returns>
		Task<CourseResponse> EditCourse(EditCourseRequest editCourseRequest);
	}
}
