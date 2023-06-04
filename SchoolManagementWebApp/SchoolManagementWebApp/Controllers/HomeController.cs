using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all home page related actions
	public class HomeController : Controller
	{
		private readonly ICourseGetterService _courseGetterService;

		public HomeController(ICourseGetterService courseGetterService) 
		{ 
			_courseGetterService = courseGetterService;
		}

		// Returns home view for /home endpoint
		[HttpGet]
		[Route("/home")]
		public async Task<IActionResult> Home()
		{
			// TODO show only user enrolled courses
			List<CourseResponse> courses = await _courseGetterService.GetAllCourses();

			ViewData["pageTitle"] = "Home";
			ViewData["Courses"] = courses;

			return View("Home");
		}
	}
}
