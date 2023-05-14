using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all course related actions
	public class CourseController : Controller
	{
		// Returns create courses view for /createcourse endpoint
		[Route("/createcourse")]
		public IActionResult CreateCourse()
		{
			return View("CreateCourse");
		}

		// Returns course view for /course endpoint
		[Route("/course")]
		public IActionResult Course()
		{
			return View("Course");
		}

		// Returns edit course view for /editcourse endpoint
		[Route("/editcourse")]
		public IActionResult EditCourse()
		{
			return View("EditCourse");
		}

		// Returns create message view for /createmessage endpoint
		[Route("/createmessage")]
		public IActionResult CreateMessage()
		{
			return View("CreateMessage");
		}

		// Returns submit assignment view for /submitassignment endpoint
		[Route("/submitassignment")]
		public IActionResult SubmitAssignment()
		{
			return View("SubmitAssignment");
		}

		// Returns submitted assignments view for /submittedassignments endpoint
		[Route("/submittedassignments")]
		public IActionResult SubmittedAssignments()
		{
			return View("SubmittedAssignments");
		}
	}
}
