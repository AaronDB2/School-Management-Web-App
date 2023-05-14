using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
    public class AccountControllerTests
    {
        [Fact]
        public void Login_ShouldReturnLoginView()
        {
            // Arrange
            AccountController accountController = new AccountController();

            // Act
            IActionResult result = accountController.Login();

            // Assert if result is of viewResult type
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            // Assert if viewname is equal to Login
            Assert.Equal("Login", viewResult.ViewName);
        }

		[Fact]
		public void Profile_ShouldReturnProfileView()
		{
			// Arrange
			AccountController accountController = new AccountController();

			// Act
			IActionResult result = accountController.Profile();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Profile
			Assert.Equal("Profile", viewResult.ViewName);
		}

		[Fact]
		public void CreateAccount_ShouldReturnCreateAccountView()
		{
			// Arrange
			AccountController accountController = new AccountController();

			// Act
			IActionResult result = accountController.CreateAccount();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateAccount
			Assert.Equal("CreateAccount", viewResult.ViewName);
		}
	}
}