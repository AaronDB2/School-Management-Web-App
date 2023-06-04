using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;

namespace SchoolManagementWebApp.UI.Controllers
{
    // Controller for all account related actions
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICoursesRepository _coursesRepository;

		// TODO: mock usermanager in tests if needed. Temporary fix
		//public AccountController(ICoursesRepository coursesRepository)
		//{

		//}

		public AccountController(UserManager<ApplicationUser> userManager, ICoursesRepository coursesRepository)
		{
			_userManager = userManager;
			_coursesRepository = coursesRepository;
		}

		// Returns login view for /login endpoint
		[HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
			ViewData["pageTitle"] = "Login";
			return View("Login");
        }

		// Returns profile view for /profile endpoint
		[HttpGet]
		[Route("/profile")]
		public IActionResult Profile()
		{
			ViewData["pageTitle"] = "Profile";
			return View("Profile");
		}

		// Updates password for ApplicationUser entity
		[HttpPost]
		[Route("/profile")]
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
		public IActionResult CreateAccount()
		{
			ViewData["pageTitle"] = "Create Account";
			return View("CreateAccount");
		}

		// Registers a new account based on the information received
		[HttpPost]
		[Route("/createaccount")]
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
