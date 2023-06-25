using Azure;
using FluentAssertions;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.Core.Services;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SchoolManagementWebApp.ServiceTests
{
	public class CourseServiceTests
	{
		private readonly ICourseAdderService _courseAdderService;
		private readonly IEditCourseService _editCourseService;
		private readonly IEditCourseMessageService _editCourseMessageService;
		private readonly ICourseGetterService _courseGetterService;

		private readonly Mock<ICoursesRepository> _coursesRepositoryMock;
		private readonly ICoursesRepository _coursesRepository;
		public CourseServiceTests() 
		{
			// Mock CoursesRepository
			_coursesRepositoryMock = new Mock<ICoursesRepository>();
			_coursesRepository = _coursesRepositoryMock.Object;

			// Initialize services
			_courseGetterService = new CourseGetterService(_coursesRepository);
			_courseAdderService = new CourseAdderService(_coursesRepository);
			_editCourseService= new EditCourseService(_coursesRepository);
			_editCourseMessageService = new EditCourseMessageService(_coursesRepository);
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
		public async Task AddCourse_CourseDetailComplete_ToBeSuccessfull()
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

		[Fact]
		public async Task EditCourse_EditCourseRequestIsNull_ToBeArgumentNullException()
		{
			//Arrange
			EditCourseRequest? editCourseRequest = null;

			//Act
			Func<Task> action = async () =>
			{
				await _editCourseService.EditCourse(editCourseRequest);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task EditCourse_EditCourseRequestFullDetails_ToBeSuccessfull()
		{
			//Arrange
			EditCourseRequest? editCourseRequest = new EditCourseRequest()
			{
				CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				CourseFileName = "UpdatedCourse.pdf",
				CourseText = "Here is the updated course information from the test"
			};

			Course course = new Course() 
			{
				CourseId = editCourseRequest.CourseId,
				CourseFileName = editCourseRequest.CourseFileName,
				CourseText = editCourseRequest.CourseText,
				CourseName = "CourseName",
				TeacherId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27")
			};

			//Mock GetCourseByCourseId method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.GetCourseByCourseId(It.IsAny<Guid>()))
			 .ReturnsAsync(course);

			//Mock UpdateCourseInfo method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.UpdateCourseInfo(It.IsAny<Course>()))
			 .ReturnsAsync(course);

			//Act
			CourseResponse response = await _editCourseService.EditCourse(editCourseRequest);
			
			//Assert
			response.Should().NotBeNull();
			response.CourseId.Should().Be(editCourseRequest.CourseId);
		}

		[Fact]
		public async Task EditCourseMessage_EditCourseMessageRequestIsNull_ToBeArgumentNullException()
		{
			//Arrange
			EditCourseMessageRequest? editCourseMessageRequest = null;

			//Act
			Func<Task> action = async () =>
			{
				await _editCourseMessageService.EditCourseMessage(editCourseMessageRequest);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task EditCourseMessage_EditCourseMessageRequestFullDetails_ToBeSuccessfull()
		{
			//Arrange
			EditCourseMessageRequest? editCourseMessageRequest = new EditCourseMessageRequest()
			{
				CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				CourseMessage = "Updated news here. JULLIE HEBBEN ALLEMAAL ONVOLDOENDE!!!!!!!!!"
			};

			Course course = new Course()
			{
				CourseId = editCourseMessageRequest.CourseId,
				CourseFileName = "Course.pdf",
				CourseText = "CourseText Here",
				CourseName = "CourseName",
				TeacherId = Guid.Parse("6E0CBCD5-3807-4813-95D1-930B9A220F27"),
				Message = editCourseMessageRequest.CourseMessage 
			};

			//Mock GetCourseByCourseId method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.GetCourseByCourseId(It.IsAny<Guid>()))
			 .ReturnsAsync(course);

			//Mock UpdateCourseMessage method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.UpdateCourseMessage(It.IsAny<Course>()))
			 .ReturnsAsync(course);

			//Act
			CourseResponse response = await _editCourseMessageService.EditCourseMessage(editCourseMessageRequest);

			//Assert
			response.Should().NotBeNull();
			response.CourseId.Should().Be(editCourseMessageRequest.CourseId);
		}

		[Fact]
		public async Task GetCourseByCourseId_CourseIdEmpty_ToBeArgumentNullException()
		{
			//Arrange
			Guid courseId = Guid.Empty;

			//Act
			Func<Task> action = async () =>
			{
				await _courseGetterService.GetCourseByCourseId(courseId);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task GetCourseByCourseId_CourseNull_ToBeArgumentNullException()
		{
			//Arrange
			Guid courseId = Guid.NewGuid();

			Course course = null;

			//Mock GetCourseByCourseId method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.GetCourseByCourseId(It.IsAny<Guid>()))
			 .ReturnsAsync(course);

			//Act
			Func<Task> action = async () =>
			{
				await _courseGetterService.GetCourseByCourseId(courseId);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task GetCourseByCourseId_CourseFoundWithCourseId_ToBeSuccessfull()
		{
			//Arrange
			Guid courseId = Guid.NewGuid();

			Course course = new Course()
			{
				CourseId = courseId,
				CourseName = "Test",
				TeacherId = Guid.NewGuid(),
			};

			//Mock GetCourseByCourseId method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.GetCourseByCourseId(It.IsAny<Guid>()))
			 .ReturnsAsync(course);

			//Act
			CourseResponse response = await _courseGetterService.GetCourseByCourseId(courseId);

			//Assert
			response.Should().NotBeNull();
			response.CourseId.Should().Be(course.CourseId);
		}

		[Fact]
		public async Task GetAllCourses_Called_ToBeSuccessfull()
		{
			//Arrange
			Course course1 = new Course()
			{
				CourseId = Guid.NewGuid(),
				CourseName = "Test1",
				TeacherId = Guid.NewGuid(),
			};

			Course course2 = new Course()
			{
				CourseId = Guid.NewGuid(),
				CourseName = "Test2",
				TeacherId = Guid.NewGuid(),
			};

			Course course3 = new Course()
			{
				CourseId = Guid.NewGuid(),
				CourseName = "Test3",
				TeacherId = Guid.NewGuid(),
			};

			List<Course> courses = new List<Course>() { course1, course2, course3 };

			List<CourseResponse> courseResponses = new List<CourseResponse>();

			courseResponses.Add(new CourseResponse()
			{
				CourseId = course1.CourseId,
				CourseName = course1.CourseName,
				TeacherId = course1.TeacherId,
			});

			courseResponses.Add(new CourseResponse()
			{
				CourseId = course2.CourseId,
				CourseName = course2.CourseName,
				TeacherId = course2.TeacherId,
			});

			courseResponses.Add(new CourseResponse()
			{
				CourseId = course3.CourseId,
				CourseName = course3.CourseName,
				TeacherId = course3.TeacherId,
			});

			//Mock GetCourseByCourseId method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.GetAllCourses())
			 .ReturnsAsync(courses);

			//Act
			List<CourseResponse> response = await _courseGetterService.GetAllCourses();

			//Assert
			response.Should().NotBeNull();

			// Loop over the response and check if the Ids are the same as the dummy courses
			for (int i = 0; i < response.Count; i++)
			{
				response[i].CourseId.Should().Be(courses[i].CourseId);
			};
		}

		[Fact]
		public async Task GetFilterdCourses_SearchByCourseId_ToBeSuccessfull()
		{
			//Arrange
			Course course1 = new Course()
			{
				CourseId = Guid.NewGuid(),
				CourseName = "Test1",
				TeacherId = Guid.NewGuid(),
			};

			List<Course> courses = new List<Course>() { course1 };

			//Mock GetFilterdCourses method from CoursesRepository 
			_coursesRepositoryMock.Setup
			 (temp => temp.GetFilterdCourses(It.IsAny<Expression<Func<Course, bool>>>()))
			 .ReturnsAsync(courses);

			//Act
			List<CourseResponse> response = await _courseGetterService.GetFilterdCourses("CourseId", course1.CourseId.ToString());

			//Assert
			response.Should().NotBeNull();
			response[0].CourseId.Should().Be(course1.CourseId);
		}
	}
}