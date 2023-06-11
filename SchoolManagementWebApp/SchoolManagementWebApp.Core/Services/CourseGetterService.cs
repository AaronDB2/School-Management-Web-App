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
	public class CourseGetterService : ICourseGetterService
	{
		private readonly ICoursesRepository _coursesRepository;

		public CourseGetterService(ICoursesRepository coursesRepository)
		{
			_coursesRepository = coursesRepository;
		}

		public async Task<List<CourseResponse>> GetAllCourses()
		{
			List<Course> courses = await _coursesRepository.GetAllCourses();

			List<CourseResponse> response = new List<CourseResponse>();

			// Convert all course into CourseResponse objects and add them to the response list
			foreach(Course course in courses) 
			{
				CourseResponse courseResponse = new CourseResponse()
				{
					CourseId = course.CourseId,
					CourseName = course.CourseName,
					CourseFileName = course.CourseFileName,
					CourseMessage = course.Message,
					TeacherId = course.TeacherId,
					Assignments = course.Assignments,
					CourseText = course.CourseText,
					Students = course.Students,
				};

				response.Add(courseResponse);
			}

			return response;
		}

		public async Task<CourseResponse> GetCourseByCourseId(Guid coursId)
		{
			// Check if courseId is empty
			if (coursId== Guid.Empty) throw new ArgumentNullException(nameof(coursId));

			Course course = await _coursesRepository.GetCourseByCourseId(coursId);

			// Check if there was a course found for given id
			if (course == null) throw new ArgumentNullException();

			CourseResponse response = new CourseResponse()
			{
				CourseId = course.CourseId,
				CourseName = course.CourseName,
				CourseFileName = course.CourseFileName,
				CourseMessage = course.Message,
				TeacherId= course.TeacherId,
				Assignments = course.Assignments,
				CourseText = course.CourseText,
				Students = course.Students,
			};

			return response;
		}

		public async Task<List<CourseResponse>> GetFilterdCourses(string searchBy, string? searchString)
		{
			List<Course> courses = searchBy switch
			{
				nameof(CourseResponse.CourseId) =>
				 await _coursesRepository.GetFilterdCourses(temp =>
				 temp.CourseId.ToString().Contains(searchString)),

				nameof(CourseResponse.CourseName) =>
				 await _coursesRepository.GetFilterdCourses(temp =>
				 temp.CourseName.Contains(searchString)),

			    nameof(CourseResponse.TeacherId) =>
				 await _coursesRepository.GetFilterdCourses(temp =>
				 temp.TeacherId.ToString().Contains(searchString)),

				_ => await _coursesRepository.GetAllCourses()
			};
			// TODO: INCLUDE TEACHERID?

			List <CourseResponse> response = new List<CourseResponse>();
			
			foreach (Course course in courses)
			{
				CourseResponse courseResponse = new CourseResponse()
				{
					CourseId = course.CourseId,
					CourseName = course.CourseName,
					Students = course.Students,
					CourseText = course.CourseText,
					CourseMessage = course.Message,
					CourseFileName = course.CourseFileName,
					Assignments = course.Assignments,
					TeacherId= course.TeacherId,
				};

				response.Add(courseResponse);
			}

			return response;
		}
	}
}
