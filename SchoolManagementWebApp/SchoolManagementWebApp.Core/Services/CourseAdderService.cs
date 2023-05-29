using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.Services
{
	/// <summary>
	/// Service for adding course to data store
	/// </summary>
	public class CourseAdderService : ICourseAdderService
	{
		private readonly ICoursesRepository _coursesRepository;

		public CourseAdderService (ICoursesRepository coursesRepository)
		{
			_coursesRepository = coursesRepository;
		}

		public async Task<CourseResponse> AddCourse(CourseAddRequest? courseAddRequest)
		{
			//CourseAddRequest parameter can't be null
			if (courseAddRequest == null)
			{
				throw new ArgumentNullException(nameof(courseAddRequest));
			}

			//CourseName can't be null
			if (courseAddRequest.CourseName == null)
			{
				throw new ArgumentNullException(nameof(courseAddRequest.CourseName));
			}

			//TODO: CountryName can't be duplicate

			//TODO: Check teacher Id valid

			//Convert object from CourseAddRequest to Course type
			Course course = courseAddRequest.ToCourse();

			//generate CourseId
			course.CourseId = Guid.NewGuid();

			//Add course object into _coursesRepository
			await _coursesRepository.AddCourse(course);

			// Generate CourseResponse
		    CourseResponse courseResponse = new CourseResponse() { CourseId = course.CourseId, CourseName = course.CourseName};

			return courseResponse;

		}
	}
}
