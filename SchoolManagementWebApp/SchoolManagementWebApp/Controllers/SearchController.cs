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
			
		// Returns search courses view for /searchcourses endpoint
		[HttpGet]
		[Route("/searchcourses")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> SearchCourses()
		{
			List<CourseResponse> data = await _courseGetterService.GetAllCourses();

			ViewData["pageTitle"] = "Search Courses";
			ViewData["Courses"] = data;

			return View("SearchCourses");
		}
	}
}
