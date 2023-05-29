using FluentAssertions;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.Core.Services;
using System.Runtime.CompilerServices;

namespace SchoolManagementWebApp.ServiceTests
{
	public class CourseServiceTests
	{
		private readonly ICourseAdderService _courseAdderService;

		private readonly Mock<ICoursesRepository> _coursesRepositoryMock;
		private readonly ICoursesRepository _coursesRepository;
		public CourseServiceTests() 
		{
			// Mock CoursesRepository
			_coursesRepositoryMock = new Mock<ICoursesRepository>();
			_coursesRepository = _coursesRepositoryMock.Object;

			// Initialize services
			_courseAdderService = new CourseAdderService(_coursesRepository);
		}
		[Fact]
		public async Task AddCourse_CourseNull_ToBeArgumentNullException()
		{
			//Arrange
			CourseAddRequest? courseAddRequest = null;

			//Act
			Func<Task> action = async () =>
			{
				await _courseAdderService.AddCourse(courseAddRequest);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task AddCourse_CourseNameIsNull_ToBeArgumentNullException()
		{
			//Arrange
			CourseAddRequest? courseAddRequest = new CourseAddRequest();

			//Act
			Func<Task> action = async () =>
			{
				await _courseAdderService.AddCourse(courseAddRequest);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task AddCourse_CourseDetailComplete_ToBeSuccessful()
		{
			//Arrange
			CourseAddRequest? courseAddRequest = new CourseAddRequest()
			{
				CourseName = "Test",
				TeacherId = Guid.NewGuid(),
			};

			Course course = courseAddRequest.ToCourse();
			CourseResponse courseResponse = new CourseResponse() { CourseId = course.CourseId, CourseName = course.CourseName };

			//Mock AddCourse method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.AddCourse(It.IsAny<Course>()))
			 .ReturnsAsync(course);

			//Act
			CourseResponse course_response_from_add = await _courseAdderService.AddCourse(courseAddRequest);

			courseResponse.CourseId = course_response_from_add.CourseId;

			//Assert
			course_response_from_add.CourseId.Should().NotBe(Guid.Empty);
			course_response_from_add.CourseId.Should().Be(courseResponse.CourseId);

		}
	}
}