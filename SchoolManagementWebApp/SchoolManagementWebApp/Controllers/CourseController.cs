using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System.Diagnostics;
using System.Net.Http;

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
		private readonly ICourseGetterService _courseGetterService;
        private readonly IAssignmentGetterService _assignmentGetterService;

        public CourseController(
			ICourseAdderService courseAdderService, 
			IAssignmentAdderService assignmentAdderService, 
			IUpdateGradeService updateGradeService, 
			IEditCourseService editCourseService, 
			IEditCourseMessageService editCourseMessageService,
			ICourseGetterService courseGetterService,
			IAssignmentGetterService assignmentGetterService
			)
		{
			_courseAdderService = courseAdderService;
			_assignmentAdderService = assignmentAdderService;
			_updateGradeService = updateGradeService;
			_editCourseService = editCourseService;
			_editCourseMessageService = editCourseMessageService;
			_courseGetterService = courseGetterService;
			_assignmentGetterService = assignmentGetterService;
		}

		// Returns create courses view for /createcourse endpoint
		[HttpGet]
		[Route("/createcourse")]
		public IActionResult CreateCourse()
		{
			ViewData["pageTitle"] = "Create Course";

			return View("CreateCourse");
		}

		[HttpPost]
		[Route("/createcourse")]
		public async Task<IActionResult> CreateCourse(CourseAddRequest courseAddRequest)
		{
			CourseResponse response = await _courseAdderService.AddCourse(courseAddRequest);

			return RedirectToAction("CreateCourse");
		}

		// Returns course view for /course endpoint
		[HttpGet]
		[Route("/course/{courseId}")]
		public async Task<IActionResult> Course(Guid courseId)
		{
			CourseResponse  response = await _courseGetterService.GetCourseByCourseId(courseId);

			ViewData["pageTitle"] = response.CourseName;
			ViewData["Course"] = response;

			return View("Course");
		}

		// Returns edit course view for /editcourse endpoint
		[HttpGet]
		[Route("/editcourse/{courseId}")]
		public IActionResult EditCourse(Guid courseId)
		{
			ViewData["pageTitle"] = "Edit Course";
			ViewData["CourseId"] = courseId;

			return View("EditCourse");
		}

		[HttpPost]
		[Route("/editcourse")]
		public async Task<IActionResult> EditCourse(EditCourseRequest editCourseRequest, IFormFile formFile)
		{
			// Read the file and upload it to the UploadedFiles folder
            string path = "";
            try
            {
                if (formFile.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, formFile.FileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }

			// Add Filename to editCourseRequest
			editCourseRequest.CourseFileName = formFile.FileName;

			// Call service for editing course information
            CourseResponse response = await _editCourseService.EditCourse(editCourseRequest);

			return RedirectToAction("Course", new { courseId = editCourseRequest.CourseId } );
		}

		// Returns create message view for /createmessage endpoint
		[HttpGet]
		[Route("/createmessage/{courseId}")]
		public IActionResult CreateMessage(Guid courseId)
		{
			ViewData["pageTitle"] = "Create Course Message";
            ViewData["courseId"] = courseId;


            return View("CreateMessage");
		}

		[HttpPost]
		[Route("/createmessage")]
		public async Task<IActionResult> CreateMessage(EditCourseMessageRequest editCourseMessageRequest)
		{
			CourseResponse response = await _editCourseMessageService.EditCourseMessage(editCourseMessageRequest);

			return RedirectToAction("Course", new { courseId = response.CourseId } );
		}

		// Returns submit assignment view for /submitassignment endpoint
		[HttpGet]
		[Route("/submitassignment/{courseId}")]
		public IActionResult SubmitAssignment(Guid courseId)
		{
			ViewData["pageTitle"] = "Submit Assignment";
            ViewData["courseId"] = courseId;

            return View("SubmitAssignment");
		}

		[HttpPost]
		[Route("/submitassignment")]
		public async Task<IActionResult> SubmitAssignment(AssignmentAddRequest assignmentAddRequest, IFormFile assignmentFile)
		{
            // Read the file and upload it to the UploadedFiles folder
            string path = "";
			try
			{
                if (assignmentFile.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
					}
                    using (var fileStream = new FileStream(Path.Combine(path, assignmentFile.FileName), FileMode.Create))
                    {
						await assignmentFile.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }

            // Add Filename to editCourseRequest
            assignmentAddRequest.AssignmentFileName = assignmentFile.FileName;

            AssignmentResponse response = await _assignmentAdderService.AddAssignment(assignmentAddRequest);

			return RedirectToAction("Course", new {courseId = assignmentAddRequest.CourseId});
		}

		// Returns submitted assignments view for /submittedassignments endpoint
		[HttpGet]
		[Route("/submittedassignments/{courseId}")]
		public async Task<IActionResult> SubmittedAssignments(Guid courseId)
		{
			List<AssignmentResponse> assignments = await _assignmentGetterService.GetAssignmentsByCourseId(courseId);

            ViewData["pageTitle"] = "Submitted Assignments";
            ViewData["courseId"] = courseId;
            ViewData["assignments"] = assignments;

            return View("SubmittedAssignments");
		}

		[HttpPost]
		[Route("/updategrade/{assignmentId}")]
		public async Task<IActionResult> UpdateGrade(UpdateGradeRequest updateGradeRequest)
		{
			AssignmentGradeResponse response = await _updateGradeService.UpdateAssignmentGrade(updateGradeRequest);
			
			return RedirectToAction("SubmittedAssignments", new { courseId = updateGradeRequest.CourseId });
		}
	}
}
