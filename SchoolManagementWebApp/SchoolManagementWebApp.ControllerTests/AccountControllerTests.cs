using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.Infrastructure.Repositories;
using SchoolManagementWebApp.UI.Controllers;
using System.Security.Claims;

namespace SchoolManagementWebApp.ControllerTests
{
    public class AccountControllerTests
    {
		private readonly AccountController _accountController;

		private readonly Mock<ICourseGetterService> _courseGetterServiceMock;
		private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
		private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;

		private readonly ICourseGetterService _courseGetterService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountControllerTests()
		{
			// Mock
			var userStore = new Mock<IUserStore<ApplicationUser>>();

			_courseGetterServiceMock = new Mock<ICourseGetterService>();
			_courseGetterService = _courseGetterServiceMock.Object;

			_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
			_userManager = _userManagerMock.Object;

			_signInManagerMock = new Mock<SignInManager<ApplicationUser>>(_userManager, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
			_signInManager = _signInManagerMock.Object;	

			_accountController = new AccountController(_userManager, _signInManager, _courseGetterService);

			// Sets the GetUserId method to always return a userId. Did this so that i dont need to mock principle claim stuff
			_accountController.GetUserId = () => "FEDDD9F4-1C6C-43B2-B9D3-672AB82CB2C6";
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
		public async void Profile_ShouldReturnProfileView()
		{
			// Arrange
			ApplicationUser user = new ApplicationUser()
			{
				UserName= "Test",
				Id = Guid.Parse("FEDDD9F4-1C6C-43B2-B9D3-672AB82CB2C6"),
			};

			//Mock FindByIdAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByIdAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			// Act
			IActionResult result = await _accountController.Profile();


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