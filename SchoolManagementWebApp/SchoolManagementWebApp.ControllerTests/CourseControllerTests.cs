using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		private readonly Mock<ICourseAdderService> _courseAdderServiceMock;
		private readonly Mock<IAssignmentAdderService> _assignmentAdderServiceMock;
		private readonly Mock<IUpdateGradeService> _updateGradeServiceMock;
		private readonly Mock<IEditCourseService> _editCourseServiceMock;
		private readonly Mock<IEditCourseMessageService> _editCourseMessageServiceMock;

		public CourseControllerTests()
		{
			// Mock
			_courseAdderServiceMock = new Mock<ICourseAdderService>();
			_assignmentAdderServiceMock = new Mock<IAssignmentAdderService>();
			_updateGradeServiceMock = new Mock<IUpdateGradeService>();
			_editCourseServiceMock = new Mock<IEditCourseService>();
			_editCourseMessageServiceMock = new Mock<IEditCourseMessageService>();

			// Use mock object
			_courseAdderService = _courseAdderServiceMock.Object;
			_assignmentAdderService = _assignmentAdderServiceMock.Object;
			_updateGradeService = _updateGradeServiceMock.Object;
			_editCourseService = _editCourseServiceMock.Object;
			_editCourseMessageService = _editCourseMessageServiceMock.Object;

			_courseController = new CourseController(
				_courseAdderService, 
				_assignmentAdderService, 
				_updateGradeService, 
				_editCourseService,
				_editCourseMessageService);
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
		public void Course_ShouldReturnCourseView()
		{
			// Act
			IActionResult result = _courseController.Course();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Course
			Assert.Equal("Course", viewResult.ViewName);
		}

		[Fact]
		public void EditCourse_ShouldReturnEditCourseView()
		{
			// Act
			IActionResult result = _courseController.EditCourse();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to EditCourse
			Assert.Equal("EditCourse", viewResult.ViewName);
		}

		[Fact]
		public void CreateMessage_ShouldReturnCreateMessageView()
		{
			// Act
			IActionResult result = _courseController.CreateMessage();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateMessage
			Assert.Equal("CreateMessage", viewResult.ViewName);
		}

		[Fact]
		public void SubmitAssignment_ShouldReturnSubmitAssignmentView()
		{
			// Act
			IActionResult result = _courseController.SubmitAssignment();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SubmitAssignment
			Assert.Equal("SubmitAssignment", viewResult.ViewName);
		}

		[Fact]
		public void SubmittedAssignments_ShouldReturnSubmittedAssignmentsView()
		{
			// Act
			IActionResult result = _courseController.SubmittedAssignments();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SubmittedAssignments
			Assert.Equal("SubmittedAssignments", viewResult.ViewName);
		}
	}
}
