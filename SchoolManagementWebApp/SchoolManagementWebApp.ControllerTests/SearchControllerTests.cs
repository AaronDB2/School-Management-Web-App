using Microsoft.AspNetCore.Mvc;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
	public class SearchControllerTests
	{
		private readonly SearchController _searchController;

		private readonly ICourseGetterService _courseGetterService;

		private readonly Mock<ICourseGetterService> _courseGetterServiceMock;
		public SearchControllerTests()
		{
			// Mock
			_courseGetterServiceMock= new Mock<ICourseGetterService>();

			// Use mock object
			_courseGetterService = _courseGetterServiceMock.Object;

			_searchController = new SearchController(_courseGetterService);

			// Sets the GetUserId method to always return a userId. Did this so that i dont need to mock principle claim stuff
			_searchController.GetUserId = () => "FEDDD9F4-1C6C-43B2-B9D3-672AB82CB2C6";
		}

		[Fact]
		public async void SearchCourses_ShouldReturnSearchCoursesView()
		{
			// Arrange
			string searchBy = "";
			string searchString = "";

			List<CourseResponse> courses = new List<CourseResponse>();

			//Mock GetFilterdCourses method from CourseGetterService
			_courseGetterServiceMock.Setup
			 (temp => temp.GetFilterdCourses(It.IsAny<string>(), It.IsAny<string>()))
			 .ReturnsAsync(courses);

			// Act
			IActionResult result = await _searchController.SearchCourses(searchBy, searchString);

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SearchCourses
			Assert.Equal("SearchCourses", viewResult.ViewName);
		}
	}
}
