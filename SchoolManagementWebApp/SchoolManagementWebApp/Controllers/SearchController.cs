using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all search pages related actions
	public class SearchController : Controller
	{
		// Returns search courses view for /searchcourses endpoint
		[HttpGet]
		[Route("/searchcourses")]
		public IActionResult SearchCourses()
		{
			return View("SearchCourses");
		}
	}
}
