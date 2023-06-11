using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all search pages related actions
	public class SearchController : Controller
	{
		private readonly ICourseGetterService _courseGetterService;

		public SearchController(ICourseGetterService courseGetterService)
		{
			_courseGetterService = courseGetterService;
		}

		[HttpGet]
		[Route("/searchcourses")]
		[Authorize(Roles = "Admin,Student,Teacher")]
		public async Task<IActionResult> SearchCourses(string searchBy, string searchString)
		{
			// Check if searchBy or searchString are not null
			if (searchBy == null || searchString == null) 
			{
				searchString = string.Empty;
				searchBy = string.Empty;
			}

			List<CourseResponse> data = await _courseGetterService.GetFilterdCourses(searchBy, searchString);

			ViewData["pageTitle"] = "Search Courses";
			ViewData["Courses"] = data;

			return View("SearchCourses");
		}
	}
}
