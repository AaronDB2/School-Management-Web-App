using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
	public class HomeControllerTests
	{
		private readonly HomeController _homeController;
		public HomeControllerTests()
		{
			_homeController = new HomeController();
		}

		[Fact]
		public void Home_ShouldReturnHomeView()
		{
			// Act
			IActionResult result = _homeController.Home();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Home
			Assert.Equal("Home", viewResult.ViewName);
		}
	}
}
