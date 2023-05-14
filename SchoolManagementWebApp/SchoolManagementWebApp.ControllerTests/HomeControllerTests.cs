using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
	public class HomeControllerTests
	{
		[Fact]
		public void Home_ShouldReturnHomeView()
		{
			// Arrange
			HomeController homeController = new HomeController();

			// Act
			IActionResult result = homeController.Home();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Home
			Assert.Equal("Home", viewResult.ViewName);
		}
	}
}
