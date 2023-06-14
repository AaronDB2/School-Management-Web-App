using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.UI.Controllers;
using System.Security.Claims;

namespace SchoolManagementWebApp.ControllerTests
{
	public class CourseControllerTests
	{
		private readonly CourseController _courseController;

		private readonly ICourseAdderService _courseAdderService;
		private readonly IAssignmentAdderService _assignmentAdderService;
		private readonly IUpdateGradeService _updateGradeService;
		private readonly IEditCourseService _editCourseService;
		private readonly IEditCourseMessageService _editCourseMessageService;
		private readonly ICourseGetterService _courseGetterService;
		private readonly IAssignmentGetterService _assignmentGetterService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFileService _fileService;

		private readonly Mock<ICourseAdderService> _courseAdderServiceMock;
		private readonly Mock<IAssignmentAdderService> _assignmentAdderServiceMock;
		private readonly Mock<IUpdateGradeService> _updateGradeServiceMock;
		private readonly Mock<IEditCourseService> _editCourseServiceMock;
		private readonly Mock<IEditCourseMessageService> _editCourseMessageServiceMock;
		private readonly Mock<ICourseGetterService> _courseGetterServiceMock;
		private readonly Mock<IAssignmentGetterService> _assignmentGetterServiceMock;
		private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
		private readonly Mock<IFileService> _fileServiceMock;

		public CourseControllerTests()
		{
			// Mock
			var userStore = new Mock<IUserStore<ApplicationUser>>();

			_courseAdderServiceMock = new Mock<ICourseAdderService>();
			_assignmentAdderServiceMock = new Mock<IAssignmentAdderService>();
			_updateGradeServiceMock = new Mock<IUpdateGradeService>();
			_editCourseServiceMock = new Mock<IEditCourseService>();
			_editCourseMessageServiceMock = new Mock<IEditCourseMessageService>();
			_courseGetterServiceMock = new Mock<ICourseGetterService>();
			_assignmentGetterServiceMock = new Mock<IAssignmentGetterService>();
			_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
			_fileServiceMock = new Mock<IFileService>();

			// Use mock object
			_courseAdderService = _courseAdderServiceMock.Object;
			_assignmentAdderService = _assignmentAdderServiceMock.Object;
			_updateGradeService = _updateGradeServiceMock.Object;
			_editCourseService = _editCourseServiceMock.Object;
			_editCourseMessageService = _editCourseMessageServiceMock.Object;
			_courseGetterService = _courseGetterServiceMock.Object;
			_assignmentGetterService = _assignmentGetterServiceMock.Object;
			_userManager = _userManagerMock.Object;
			_fileService = _fileServiceMock.Object;

			_courseController = new CourseController(
				_courseAdderService, 
				_assignmentAdderService, 
				_updateGradeService, 
				_editCourseService,
				_editCourseMessageService,
				_courseGetterService,
				_assignmentGetterService,
				_userManager,
				_fileService);

			// Sets the GetUserId method to always return a userId. Did this so that i dont need to mock principle claim stuff
			_courseController.GetUserId = () => "FEDDD9F4-1C6C-43B2-B9D3-672AB82CB2C6";
		}

		[Fact]
		public void CreateCourse_ShouldReturnCreateCourseView()
		{
			// Act
			IActionResult result = _courseController.CreateCourse();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateCourses
			Assert.Equal("CreateCourse", viewResult.ViewName);
		}

		[Fact]
		public async void Course_ShouldReturnCourseView()
		{
			// Arrange
			Guid courseId = Guid.Parse("75D6E88F-87DC-493C-B76E-6417EE8447F7");

			CourseResponse course = new CourseResponse();

			//Mock GetCourseByCourseId method from CourseGetterService
			_courseGetterServiceMock.Setup
			 (temp => temp.GetCourseByCourseId(It.IsAny<Guid>()))
			 .ReturnsAsync(course);

			// Act
			IActionResult result = await _courseController.Course(courseId);

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Course
			Assert.Equal("Course", viewResult.ViewName);
		}

		[Fact]
		public void EditCourse_ShouldReturnEditCourseView()
		{
			// Arrange
			Guid courseId = Guid.Parse("75D6E88F-87DC-493C-B76E-6417EE8447F7");

			// Act
			IActionResult result = _courseController.EditCourse(courseId);

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to EditCourse
			Assert.Equal("EditCourse", viewResult.ViewName);
		}

		[Fact]
		public void CreateMessage_ShouldReturnCreateMessageView()
		{
			// Arrange
			Guid courseId = Guid.Parse("75D6E88F-87DC-493C-B76E-6417EE8447F7");

			// Act
			IActionResult result = _courseController.CreateMessage(courseId);

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateMessage
			Assert.Equal("CreateMessage", viewResult.ViewName);
		}

		[Fact]
		public void SubmitAssignment_ShouldReturnSubmitAssignmentView()
		{
			// Arrange
			Guid courseId = Guid.Parse("75D6E88F-87DC-493C-B76E-6417EE8447F7");

			// Act
			IActionResult result = _courseController.SubmitAssignment(courseId);

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SubmitAssignment
			Assert.Equal("SubmitAssignment", viewResult.ViewName);
		}

		[Fact]
		public async void SubmittedAssignments_ShouldReturnSubmittedAssignmentsView()
		{
			// Arrange
			Guid courseId = Guid.Parse("75D6E88F-87DC-493C-B76E-6417EE8447F7");

			List<AssignmentResponse> assignments = new List<AssignmentResponse>();

			string searchString = "";

			//Mock GetFilterdAssignments method from AssignmentGetterService
			_assignmentGetterServiceMock.Setup
			 (temp => temp.GetFilterdAssignments(It.IsAny<string>(), It.IsAny<string>()))
			 .ReturnsAsync(assignments);

			// Act
			IActionResult result = await _courseController.SubmittedAssignments(courseId, searchString);

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SubmittedAssignments
			Assert.Equal("SubmittedAssignments", viewResult.ViewName);
		}
	}
}
