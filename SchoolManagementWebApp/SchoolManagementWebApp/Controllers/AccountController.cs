using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementWebApp.UI.Controllers
{
    // Controller for all account related actions
    public class AccountController : Controller
    {
        // Returns login view for /login endpoint
        [Route("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

		// Returns profile view for /profile endpoint
		[Route("/profile")]
		public IActionResult Profile()
		{
			return View("Profile");
		}

		// Returns create account view for /createaccount endpoint
		[Route("/createaccount")]
		public IActionResult CreateAccount()
		{
			return View("CreateAccount");
		}
	}
}
