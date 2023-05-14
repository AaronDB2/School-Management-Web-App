using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.ControllerTests
{
	public class CoursesControllerTests
	{
		[Fact]
		public void CreateCourse_ShouldReturnCreateCourseView()
		{
			// Arrange
			CourseController coursesController = new CourseController();

			// Act
			IActionResult result = coursesController.CreateCourse();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateCourses
			Assert.Equal("CreateCourse", viewResult.ViewName);
		}

		[Fact]
		public void Course_ShouldReturnCourseView()
		{
			// Arrange
			CourseController coursesController = new CourseController();

			// Act
			IActionResult result = coursesController.Course();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Course
			Assert.Equal("Course", viewResult.ViewName);
		}

		[Fact]
		public void EditCourse_ShouldReturnEditCourseView()
		{
			// Arrange
			CourseController coursesController = new CourseController();

			// Act
			IActionResult result = coursesController.EditCourse();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to EditCourse
			Assert.Equal("EditCourse", viewResult.ViewName);
		}

		[Fact]
		public void CreateMessage_ShouldReturnCreateMessageView()
		{
			// Arrange
			CourseController coursesController = new CourseController();

			// Act
			IActionResult result = coursesController.CreateMessage();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateMessage
			Assert.Equal("CreateMessage", viewResult.ViewName);
		}

		[Fact]
		public void SubmitAssignment_ShouldReturnSubmitAssignmentView()
		{
			// Arrange
			CourseController coursesController = new CourseController();

			// Act
			IActionResult result = coursesController.SubmitAssignment();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SubmitAssignment
			Assert.Equal("SubmitAssignment", viewResult.ViewName);
		}

		[Fact]
		public void SubmittedAssignments_ShouldReturnSubmittedAssignmentsView()
		{
			// Arrange
			CourseController coursesController = new CourseController();

			// Act
			IActionResult result = coursesController.SubmittedAssignments();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SubmittedAssignments
			Assert.Equal("SubmittedAssignments", viewResult.ViewName);
		}
	}
}
