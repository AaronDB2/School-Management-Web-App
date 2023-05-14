using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all home page related actions
	public class HomeController : Controller
	{
		// Returns home view for /home endpoint
		[Route("/home")]
		public IActionResult Home()
		{
			return View("Home");
		}
	}
}
