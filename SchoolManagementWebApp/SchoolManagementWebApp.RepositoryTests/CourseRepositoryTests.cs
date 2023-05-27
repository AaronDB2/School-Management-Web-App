using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Infrastructure.DbContext;
using SchoolManagementWebApp.Infrastructure.Repositories;

namespace SchoolManagementWebApp.RepositoryTests
{
	public class CourseRepositoryTests
	{
		private readonly CoursesRepository _coursesRepository;

		// Initial data store data
		private List<Course> coursesInitialData = new List<Course>() { new Course
		{
			CourseId = new Guid("2D64B2A9-6469-4D98-8C10-60640B9F8475"),
			CourseName = "Test Course2",
			Message = "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"",
			CourseText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
			CourseFileName = "Test.pdf",
			TeacherId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27")
		}, new Course
		{
			CourseId = new Guid("C8BCB438-93C5-4A93-A117-E8AD77A936E3"),
			CourseName = "Test Course3",
			Message = "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"",
			CourseText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
			CourseFileName = "Test.pdf",
			TeacherId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27")
		}};

		/// <summary>
		/// Constructor for initializing private variables and mocking
		/// </summary>
		public CourseRepositoryTests() 
		{
			// Mock the dbContext
			DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
			var dbContext = dbContextMock.Object;

			// Mock db sets
			dbContextMock.CreateDbSetMock(temp => temp.Courses, coursesInitialData);

			_coursesRepository = new CoursesRepository(dbContext);
		}

		[Fact]
		public async Task AddCourse_ShouldAddNewCourseObjectToDataStore()
		{
			var addedCourse = await _coursesRepository.AddCourse(new Course
			{
				CourseId = new Guid("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				CourseName = "Test Course",
				Message = "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"",
				CourseText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
				CourseFileName = "Test.pdf",
				TeacherId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27")
			});

			// TODO i am calling a second method here so this test is technically not valid as i test a second method here aswell. Should changes it later
			var returnedCourse = await _coursesRepository.GetCourseByCourseId(Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"));

			Assert.Equal(returnedCourse, addedCourse);
		}

		[Fact]
		public async Task GetCourseByCourseId_ShouldReturnCourseWithCorrectIdFromDataStore()
		{
			var returnedCourse = await _coursesRepository.GetCourseByCourseId(Guid.Parse("2D64B2A9-6469-4D98-8C10-60640B9F8475"));

			Assert.Equal(returnedCourse.CourseId, Guid.Parse("2D64B2A9-6469-4D98-8C10-60640B9F8475"));
		}

		[Fact]
		public async Task GetAllCourses_ShouldReturnAllCourseFromDataStore()
		{
			var returnedCourses = await _coursesRepository.GetAllCourses();

			// Check if returnedCourses is of IEnumerable type
			Assert.IsAssignableFrom<IEnumerable<Course>>(returnedCourses);

			// Check if returnedCourses and coursesInitialData are the same size
			Assert.Equal(returnedCourses.Count, coursesInitialData.Count);

			// Check if returnedCourses and coursesInitialData first course in collection have the same Id
			Assert.Equal(returnedCourses[0].CourseId, coursesInitialData[0].CourseId);
		}

		[Fact]
		public async Task GetFilterdCourses_ShoudReturnAListOfAllCoursesThatMatchTheExpression()
		{
			var returnedCourses = await _coursesRepository.GetFilterdCourses(temp => temp.CourseName.Contains("Test Course2"));

			// Check if returnedCourses is of IEnumerable type
			Assert.IsAssignableFrom<IEnumerable<Course>>(returnedCourses);

			// Check if first item in returnedCourses has the CourseName "Test Course2"
			Assert.Equal(returnedCourses[0].CourseName, "Test Course2");
		}

		[Fact]
		public async Task UpdateCourseInfo_ShoudUpdateCourseTextAndFileNameForASingleEntityInDataStore()
		{
			Course courseToBeUpdated = coursesInitialData[0];

			courseToBeUpdated.CourseFileName = "Updated.pdf";
			courseToBeUpdated.CourseText = "Updated Text!";

			var returnedCourse = await _coursesRepository.UpdateCourseInfo(courseToBeUpdated);

			// Check if returnedCourse has the updated courseText
			Assert.Equal(returnedCourse.CourseText, "Updated Text!");

			// Check if returnedCourse has the updated courseFileName
			Assert.Equal(returnedCourse.CourseFileName, "Updated.pdf");
		}

		[Fact]
		public async Task UpdateCourseMessage_ShoudUpdateMessageForASingleEntityInDataStore()
		{
			Course courseToBeUpdated = coursesInitialData[0];

			courseToBeUpdated.Message = "Updated Message";

			var returnedCourse = await _coursesRepository.UpdateCourseMessage(courseToBeUpdated);

			//Check if returnedCourse has the updated message
			Assert.Equal(returnedCourse.Message, "Updated Message");
		}
	}
}