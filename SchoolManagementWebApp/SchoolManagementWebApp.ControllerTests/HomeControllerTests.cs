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
		private readonly IFileService _fileService;

		private readonly Mock<ICourseGetterService> _courseGetterServiceMock;
		private readonly Mock<IFileService> _fileServiceMock;

		public HomeControllerTests()
		{
			// Mock
			_courseGetterServiceMock = new Mock<ICourseGetterService>();
			_fileServiceMock = new Mock<IFileService>();

			// Use mock object
			_courseGetterService = _courseGetterServiceMock.Object;
			_fileService = _fileServiceMock.Object;

			_homeController = new HomeController(_courseGetterService, _fileService);

			// Sets the GetUserId method to always return a userId. Did this so that i dont need to mock principle claim stuff
			_homeController.GetUserId = () => "FEDDD9F4-1C6C-43B2-B9D3-672AB82CB2C6";
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
