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
	public class EditCourseMessageService : IEditCourseMessageService
	{
		private readonly ICoursesRepository _coursesRepository;
		public EditCourseMessageService(ICoursesRepository coursesRepository) 
		{ 
			_coursesRepository= coursesRepository;
		}

		public async Task<CourseResponse> EditCourseMessage(EditCourseMessageRequest editCourseMessageRequest)
		{
			// Check if editCourseMessageRequest is null
			if (editCourseMessageRequest == null)
			{
				throw new ArgumentNullException(nameof(editCourseMessageRequest));
			}

			// Get course from data store
			Course courseToBeUpdated = await _coursesRepository.GetCourseByCourseId(editCourseMessageRequest.CourseId);

			courseToBeUpdated.Message = editCourseMessageRequest.CourseMessage;

			// Check if course was found
			if (courseToBeUpdated == null)
			{
				throw new ArgumentNullException(nameof(courseToBeUpdated));
			}

			// Update course
			Course courseUpdated = await _coursesRepository.UpdateCourseMessage(courseToBeUpdated);

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
