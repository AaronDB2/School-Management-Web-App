using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Infrastructure.Repositories;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
    public class AccountControllerTests
    {
		private readonly AccountController _accountController;

		private readonly Mock<ICoursesRepository> _coursesRepositoryMock;

		private readonly ICoursesRepository _coursesRepository;

		public AccountControllerTests()
		{
			_coursesRepositoryMock = new Mock<ICoursesRepository>();
			_coursesRepository = _coursesRepositoryMock.Object;

			_accountController = new AccountController(_coursesRepository);
		}
        [Fact]
        public void Login_ShouldReturnLoginView()
        { 
            // Act
            IActionResult result = _accountController.Login();

            // Assert if result is of viewResult type
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            // Assert if viewname is equal to Login
            Assert.Equal("Login", viewResult.ViewName);
        }

		[Fact]
		public void Profile_ShouldReturnProfileView()
		{
			// Act
			IActionResult result = _accountController.Profile();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to Profile
			Assert.Equal("Profile", viewResult.ViewName);
		}

		[Fact]
		public void CreateAccount_ShouldReturnCreateAccountViewOnGetRequest()
		{
			// Act
			IActionResult result = _accountController.CreateAccount();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to CreateAccount
			Assert.Equal("CreateAccount", viewResult.ViewName);
		}
	}
}