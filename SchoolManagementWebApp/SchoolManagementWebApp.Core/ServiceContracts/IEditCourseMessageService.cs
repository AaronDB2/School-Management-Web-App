using SchoolManagementWebApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.ServiceContracts
{
	public interface IEditCourseMessageService
	{
		/// <summary>
		/// Validates request data and calls repository for updating the course message in data store
		/// </summary>
		/// <param name="editCourseMessageRequest">Course to be updated</param>
		/// <returns>Course that was updated</returns>
		Task<CourseResponse> EditCourseMessage(EditCourseMessageRequest editCourseMessageRequest);
	}
}
