using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.UI.Controllers;

namespace SchoolManagementWebApp.ControllerTests
{
	public class SearchControllerTests
	{
		private readonly SearchController _searchController;
		public SearchControllerTests()
		{
			_searchController = new SearchController();
		}

		[Fact]
		public void SearchCourses_ShouldReturnSearchCoursesView()
		{
			// Act
			IActionResult result = _searchController.SearchCourses();

			// Assert if result is of viewResult type
			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			// Assert if viewname is equal to SearchCourses
			Assert.Equal("SearchCourses", viewResult.ViewName);
		}
	}
}
