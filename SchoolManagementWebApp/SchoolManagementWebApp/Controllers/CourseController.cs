using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System.Runtime.CompilerServices;

namespace SchoolManagementWebApp.UI.Controllers
{
	// Controller for all course related actions
	public class CourseController : Controller
	{
		// Services
		private readonly ICourseAdderService _courseAdderService;
		private readonly IAssignmentAdderService _assignmentAdderService;
		private readonly IUpdateGradeService _updateGradeService;
		private readonly IEditCourseService _editCourseService;
		private readonly IEditCourseMessageService _editCourseMessageService;

		public CourseController(
			ICourseAdderService courseAdderService, 
			IAssignmentAdderService assignmentAdderService, 
			IUpdateGradeService updateGradeService, 
			IEditCourseService editCourseService, 
			IEditCourseMessageService editCourseMessageService
			)
		{
			_courseAdderService = courseAdderService;
			_assignmentAdderService = assignmentAdderService;
			_updateGradeService = updateGradeService;
			_editCourseService = editCourseService;
			_editCourseMessageService = editCourseMessageService;
		}

		// Returns create courses view for /createcourse endpoint
		[HttpGet]
		[Route("/createcourse")]
		public IActionResult CreateCourse()
		{
			return View("CreateCourse");
		}

		[HttpPost]
		[Route("/createcourse")]
		public async Task<CourseResponse> CreateCourse(CourseAddRequest courseAddRequest)
		{
			CourseResponse response = await _courseAdderService.AddCourse(courseAddRequest);

			return response;
		}

		// Returns course view for /course endpoint
		[HttpGet]
		[Route("/course")]
		public IActionResult Course()
		{
			return View("Course");
		}

		// Returns edit course view for /editcourse endpoint
		[HttpGet]
		[Route("/editcourse")]
		public IActionResult EditCourse()
		{
			return View("EditCourse");
		}

		[HttpPost]
		[Route("/editcourse")]
		public async Task<CourseResponse> EditCourse(EditCourseRequest editCourseRequest)
		{
			CourseResponse response = await _editCourseService.EditCourse(editCourseRequest);

			return response;
		}

		// Returns create message view for /createmessage endpoint
		[HttpGet]
		[Route("/createmessage")]
		public IActionResult CreateMessage()
		{
			return View("CreateMessage");
		}

		[HttpPost]
		[Route("/createmessage")]
		public async Task<CourseResponse> CreateMessage(EditCourseMessageRequest editCourseMessageRequest)
		{
			CourseResponse response = await _editCourseMessageService.EditCourseMessage(editCourseMessageRequest);

			return response;
		}

		// Returns submit assignment view for /submitassignment endpoint
		[HttpGet]
		[Route("/submitassignment")]
		public IActionResult SubmitAssignment()
		{
			return View("SubmitAssignment");
		}

		[HttpPost]
		[Route("/submitassignment")]
		public async Task<AssignmentResponse> SubmitAssignment(AssignmentAddRequest assignmentAddRequest)
		{
			AssignmentResponse response = await _assignmentAdderService.AddAssignment(assignmentAddRequest);

			return response;
		}

		// Returns submitted assignments view for /submittedassignments endpoint
		[HttpGet]
		[Route("/submittedassignments")]
		public IActionResult SubmittedAssignments()
		{
			return View("SubmittedAssignments");
		}

		[HttpPost]
		[Route("/updategrade")]
		public async Task<AssignmentGradeResponse> UpdateGrade(UpdateGradeRequest updateGradeRequest)
		{
			AssignmentGradeResponse response = await _updateGradeService.UpdateAssignmentGrade(updateGradeRequest);
			
			return response;
		}
	}
}
