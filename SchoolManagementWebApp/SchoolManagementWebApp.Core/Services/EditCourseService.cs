using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.Services
{
	public class EditCourseService : IEditCourseService
	{
		private readonly ICoursesRepository _coursesRepository;

		public EditCourseService(ICoursesRepository coursesRepository)
		{
			_coursesRepository = coursesRepository;
		}

		public async Task<CourseResponse> EditCourse(EditCourseRequest editCourseRequest)
		{
			// Check if editCourseRequest is null
			if (editCourseRequest == null)
			{
				throw new ArgumentNullException(nameof(editCourseRequest));
			}

			// Get course from data store
			Course courseToBeUpdated = await _coursesRepository.GetCourseByCourseId(editCourseRequest.CourseId);

			courseToBeUpdated.CourseText = editCourseRequest.CourseText;
			courseToBeUpdated.CourseFileName = editCourseRequest.CourseFileName;

			// Check if course was found
			if (courseToBeUpdated == null)
			{
				throw new ArgumentNullException(nameof(courseToBeUpdated));
			}
			
			// Update course
			Course courseUpdated = await _coursesRepository.UpdateCourseInfo(courseToBeUpdated);

			// Check if course was found
			if (courseUpdated == null)
			{
				throw new ArgumentNullException(nameof(courseUpdated));
			}

			CourseResponse response = new CourseResponse()
			{
				CourseId = courseUpdated.CourseId,
				CourseName = courseUpdated.CourseName,
			}; 

			return response;


		}
	}
}
