using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
	public class HomeControllerTests
	{
		private readonly HomeController _homeController;

		private readonly ICourseGetterService _courseGetterService;

		private readonly Mock<ICourseGetterService> _courseGetterServiceMock;

		public HomeControllerTests()
		{
			// Mock
			_courseGetterServiceMock= new Mock<ICourseGetterService>();

			// Use mock object
			_courseGetterService = _courseGetterServiceMock.Object;

			_homeController = new HomeController(_courseGetterService);
		}

		[Fact]
		public async void Home_ShouldReturnHomeView()
		{
			//Arrange
			List<CourseResponse> courses = new List<CourseResponse>();

			//Mock GetAllCourses method from CourseGetterService
			_courseGetterServiceMock.Setup
			 (temp => temp.GetAllCourses())
			 .ReturnsAsync(courses);

			// Act
			IActionResult result = await _homeController.Home();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Home
			Assert.Equal("Home", viewResult.ViewName);
		}
	}
}
