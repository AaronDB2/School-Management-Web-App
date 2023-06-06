using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System.Text;

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
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> Home()
		{
			// TODO show only user enrolled courses
			List<CourseResponse> courses = await _courseGetterService.GetAllCourses();

			ViewData["pageTitle"] = "Home";
			ViewData["Courses"] = courses;

			return View("Home");
		}

		[HttpGet]
		[Route("/download/{filename}")]
		[Authorize(Roles = "Admin,Student,Teacher")]
		public async Task<IActionResult> Download(string fileName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
			var stream = new FileStream(path, FileMode.Open);

			return File(stream, "application/octet-stream", fileName);
		}
	}
}
