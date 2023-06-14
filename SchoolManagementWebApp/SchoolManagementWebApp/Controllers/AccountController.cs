using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using System.Security.Claims;

namespace SchoolManagementWebApp.UI.Controllers
{
    // Controller for all account related actions
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public Func<string> GetUserId { get; set; }

		private readonly ICoursesRepository _coursesRepository;

		public AccountController(UserManager<ApplicationUser> userManager, ICoursesRepository coursesRepository, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_coursesRepository = coursesRepository;
			_signInManager = signInManager;

			GetUserId = () => User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		// Returns login view for /login endpoint
		[HttpGet]
        [Route("/login")]
		[AllowAnonymous]
        public IActionResult Login()
        {
			ViewData["pageTitle"] = "Login";
			return View("Login");
        }

        // Returns login view for /login endpoint
        [HttpPost]
        [Route("/login")]
		[AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {
            ViewData["pageTitle"] = "Login";

            // Check model state
            if (!ModelState.IsValid)
			{
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View("Login", loginDTO);
            }

			// Find if there is a user with the given email
			var user = await _userManager.FindByEmailAsync(loginDTO.Email);

			if (user == null)
			{
				ModelState.AddModelError("Login", "Invalid email or password");
				return View("Login");
			}

			// Try to sign in with user email and password
			var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

			// If password and email are correct go to home screen
			if(result.Succeeded)
			{
				// Check if there is a return url and that it is a local url
				if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
				{
					return LocalRedirect(ReturnUrl);
				}
				return RedirectToAction("Home", "Home");
			} else
			{
				ModelState.AddModelError("Login", "Invalid email or password");
				return View("Login");
			}
        }

        // Returns profile view for /profile endpoint
        [HttpGet]
		[Route("/profile")]
		[Authorize(Roles = "Admin,Student,Teacher")]
		public async Task<IActionResult> Profile()
		{
            // Get the logged in userId
			var userId = GetUserId();
            //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

			ApplicationUser user = await _userManager.FindByIdAsync(userId);

			ViewData["user"] = user;
            ViewData["pageTitle"] = "Profile";
			return View("Profile");
		}

		// Updates password for ApplicationUser entity
		[HttpPost]
		[Route("/profile")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> Profile(UpdatePasswordRequest updatePasswordRequest)
		{
			// Check if updatePasswordRequest is null
			if (updatePasswordRequest == null)
			{
				throw new ArgumentNullException(nameof(updatePasswordRequest));
			}

			// Check if model state is valid
			if (!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
				return View("Profile");
			}

			// Convert user id from Guid to string
			string studentId = updatePasswordRequest.UserId.ToString();

			// Get current user from data store
			ApplicationUser user = await _userManager.FindByIdAsync(studentId);


			IdentityResult result = await _userManager.ChangePasswordAsync(user, updatePasswordRequest.CurrentPassword, updatePasswordRequest.Password);

			// Check if password change was success
			if(!result.Succeeded)
			{
				// Loops over errors but only returns the first error
				foreach(var error in result.Errors)
				{
					ViewBag.Errors = error.Description;
					return View("Profile");
				}
			}

			return RedirectToAction("Profile");
		}

		// Returns create account view for /createaccount endpoint
		[HttpGet]
		[Route("/createaccount")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAccount()
		{
			ViewData["pageTitle"] = "Create Account";
			return View("CreateAccount");
		}

		// Registers a new account based on the information received
		[HttpPost]
		[Route("/createaccount")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAccount(RegisterDTO registerDTO)
		{
			// Create new ApplicationUser based on data from RegisterDTO
			ApplicationUser user = new ApplicationUser();
			user.UserName = registerDTO.UserName;
			user.Email = registerDTO.Email;

			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

			// Checks if new user is made
			if (result.Succeeded) 
			{
				// Give every new user the Student Role
				IdentityResult resultAddedToRole = await _userManager.AddToRoleAsync(user, "Student");

				//TODO: check results

				if (registerDTO.Admin != null)
				{
					resultAddedToRole = await _userManager.AddToRoleAsync(user, "Admin");
				}

				if (registerDTO.Teacher != null) 
				{
					resultAddedToRole = await _userManager.AddToRoleAsync(user, "Teacher");
				}

				return RedirectToAction("CreateAccount");
			} else
			{
				//foreach (IdentityError error in result.Errors) 
				//{
				//	//TODO: Add errors to modelstate
				//}
			}

			return RedirectToAction("CreateAccount");
		}

		/// <summary>
		/// Enroll a student to a course by updating the user
		/// </summary>
		/// <param name="enrollStudentRequest">Data for adding student to course</param>
		/// <returns>Result of the update method</returns>
		[HttpPost]
		[Route("/enrollstudent")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> EnrollStudent(EnrollStudentRequest enrollStudentRequest)
		{
			// Convert student id from Guid to string
			string studentId = enrollStudentRequest.StudentId.ToString();

			// Find student with given id
			ApplicationUser user = await _userManager.FindByIdAsync(studentId);

			// TODO: Check when user is null

			// Get course from data store with given course id
			Course course = await _coursesRepository.GetCourseByCourseId(enrollStudentRequest.CourseId);

			// TODO: Check when course is null

			// Add student to course
			user.Courses.Add(course);

			IdentityResult result = await _userManager.UpdateAsync(user);

			// TODO: what to do when failed
			// TODO: redirect to enrolled course

			return RedirectToAction("Course", "Course", new { courseId = course.CourseId } );
		}
	}
}
