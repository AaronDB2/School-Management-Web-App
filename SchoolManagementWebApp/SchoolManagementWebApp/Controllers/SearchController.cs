using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System.Security.Claims;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all search pages related actions
	public class SearchController : Controller
	{
		private readonly ICourseGetterService _courseGetterService;

		public Func<string> GetUserId { get; set; }

		public SearchController(ICourseGetterService courseGetterService)
		{
			_courseGetterService = courseGetterService;

			GetUserId = () => User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		[HttpGet]
		[Route("/searchcourses")]
		[Authorize(Roles = "Admin,Student,Teacher")]
		public async Task<IActionResult> SearchCourses(string searchBy, string searchString)
		{
			// Check if searchBy or searchString are null
			if (searchBy == null || searchString == null) 
			{
				searchString = string.Empty;
				searchBy = string.Empty;
			}

			List<CourseResponse> filterdCourses = await _courseGetterService.GetFilterdCourses(searchBy, searchString);
			List<CourseResponse> notCurrentlyEnrolledCourses = new List<CourseResponse>();

			// Loops over all filterdCourses
			foreach (var course in filterdCourses)
			{
				// Checks if course has students
				if (course.Students.Count == 0 && course.TeacherId != Guid.Parse(GetUserId())) 
				{
					notCurrentlyEnrolledCourses.Add(course);

				} else
				{
					// Checks if student is enrolled
					bool enrolled = false;
					foreach (var student in course.Students)
					{
						if (student.Id == Guid.Parse(GetUserId()))
						{
							enrolled = true;
							break;
						} else if (course.TeacherId == Guid.Parse(GetUserId())) 
						{ 
							enrolled = true; 
							break; 
						}
					}

					// adds course to notCurrentlyEnrolledCourses if enrolled = false
					if (!enrolled)
					{
						notCurrentlyEnrolledCourses.Add(course);
					}
				}
			}

			ViewData["pageTitle"] = "Search Courses";
			ViewData["Courses"] = notCurrentlyEnrolledCourses;
			ViewData["UserId"] = GetUserId();

			return View("SearchCourses");
		}
	}
}
