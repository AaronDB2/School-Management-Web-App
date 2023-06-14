using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System.Security.Claims;
using System.Text;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all home page related actions
	public class HomeController : Controller
	{
		private readonly ICourseGetterService _courseGetterService;
		private readonly IFileService _downloadService;

		public Func<string> GetUserId { get; set; }

		public HomeController(ICourseGetterService courseGetterService, IFileService downloadService) 
		{ 
			_courseGetterService = courseGetterService;
			_downloadService = downloadService;

			GetUserId = () => User.FindFirstValue(ClaimTypes.NameIdentifier);

		}

		// Returns home view for /home endpoint
		[HttpGet]
		[Route("/home")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> Home()
		{
			// TODO show only user enrolled courses
			List<CourseResponse> courses = await _courseGetterService.GetAllCourses();
			List<CourseResponse> userEnrolledCourses = new List<CourseResponse>();

			// Checks for courses that the current user is enrolled in
			foreach (var course in courses) 
			{
				foreach(var student in course.Students)
				{
					if (student.Id == Guid.Parse(GetUserId()))
					{
						userEnrolledCourses.Add(course);
					} 
					else if (course.TeacherId == Guid.Parse(GetUserId())) 
					{
						userEnrolledCourses.Add(course);
					}
				}
			}

			ViewData["pageTitle"] = "Home";
			ViewData["Courses"] = userEnrolledCourses;

			return View("Home");
		}

		[HttpGet]
		[Route("/download/{filename}")]
		[Authorize(Roles = "Admin,Student,Teacher")]
		public async Task<IActionResult> Download(string fileName)
		{
			var stream = _downloadService.Download(fileName);

			return File(stream, "application/octet-stream", fileName);
		}
	}
}
