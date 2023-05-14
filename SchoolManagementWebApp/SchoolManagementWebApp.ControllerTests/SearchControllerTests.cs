using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
	public class SearchControllerTests
	{
		[Fact]
		public void SearchCourses_ShouldReturnSearchCoursesView()
		{
			// Arrange
			SearchController searchController = new SearchController();

			// Act
			IActionResult result = searchController.SearchCourses();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SearchCourses
			Assert.Equal("SearchCourses", viewResult.ViewName);
		}
	}
}
