using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.DTO;

namespace SchoolManagementWebApp.UI.Controllers
{
    // Controller for all account related actions
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		// Returns login view for /login endpoint
		[HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

		// Returns profile view for /profile endpoint
		[HttpGet]
		[Route("/profile")]
		public IActionResult Profile()
		{
			return View("Profile");
		}

		// Returns create account view for /createaccount endpoint
		[HttpGet]
		[Route("/createaccount")]
		public IActionResult CreateAccount()
		{
			return View("CreateAccount");
		}

		// Registers a new account based on the information received
		[HttpPost]
		[Route("/createaccount")]
		public async Task<IdentityResult> CreateAccount(RegisterDTO registerDTO)
		{
			// Create new ApplicationUser based on data from RegisterDTO
			ApplicationUser user = new ApplicationUser();
			user.UserName = registerDTO.UserName;
			user.Email = registerDTO.Email;

			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

			if (result.Succeeded) 
			{
				// TODO: do something when success
			} else
			{
				foreach (IdentityError error in result.Errors) 
				{
					//TODO: Add errors to modelstate
				}
			}

			return result;
		}
	}
}
