﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.Core.Services;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;

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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFileService _fileService;

		public Func<string> GetUserId { get; set; }

		public CourseController(
			ICourseAdderService courseAdderService, 
			IAssignmentAdderService assignmentAdderService, 
			IUpdateGradeService updateGradeService, 
			IEditCourseService editCourseService, 
			IEditCourseMessageService editCourseMessageService,
			ICourseGetterService courseGetterService,
			IAssignmentGetterService assignmentGetterService,
			UserManager<ApplicationUser> userManager,
			IFileService fileService
			)
		{
			_courseAdderService = courseAdderService;
			_assignmentAdderService = assignmentAdderService;
			_updateGradeService = updateGradeService;
			_editCourseService = editCourseService;
			_editCourseMessageService = editCourseMessageService;
			_courseGetterService = courseGetterService;
			_assignmentGetterService = assignmentGetterService;
			_userManager = userManager;
			_fileService = fileService;

			GetUserId = () => User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		// Returns create courses view for /createcourse endpoint
		[HttpGet]
		[Route("/createcourse")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCourse()
		{
			ViewData["pageTitle"] = "Create Course";		
			return View("CreateCourse");
		}

		[HttpPost]
		[Route("/createcourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse(CourseAddRequest courseAddRequest)
		{
			CourseResponse response = await _courseAdderService.AddCourse(courseAddRequest);

			return RedirectToAction("CreateCourse");
		}

		// Returns course view for /course endpoint
		[HttpGet]
		[Route("/course/{courseId}")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> Course(Guid courseId)
		{
			// Get the logged in userId
			Guid userId = Guid.Parse(GetUserId());

			CourseResponse response = await _courseGetterService.GetCourseByCourseId(courseId);

			// Check if the current logged in user is the teacher of the course
			if (response.TeacherId == userId)
			{
				ViewData["isTeacher"] = "true";
			} else
			{
				ViewData["isTeacher"] = "false";

				// Check if current logged in user has an assignment for this course
				if (response.Assignments != null)
				{
					// Get the assignment of the user
					foreach (Assignment assignment in response.Assignments)
					{
						if (assignment.StudentId == userId)
						{
							ViewData["userAssignment"] = assignment;
						}
					}
				}
			}

            ViewData["pageTitle"] = response.CourseName;
			ViewData["Course"] = response;

			return View("Course");
		}

		// Returns edit course view for /editcourse endpoint
		[HttpGet]
		[Route("/editcourse/{courseId}")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public IActionResult EditCourse(Guid courseId)
		{
			ViewData["pageTitle"] = "Edit Course";
			ViewData["CourseId"] = courseId;

			return View("EditCourse");
		}

		[HttpPost]
		[Route("/editcourse")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> EditCourse(EditCourseRequest editCourseRequest, IFormFile formFile)
		{

			await _fileService.UploadFile(formFile);

			// Add Filename to editCourseRequest
			editCourseRequest.CourseFileName = formFile.FileName;

			// Call service for editing course information
            CourseResponse response = await _editCourseService.EditCourse(editCourseRequest);

			return RedirectToAction("Course", new { courseId = editCourseRequest.CourseId } );
		}

		// Returns create message view for /createmessage endpoint
		[HttpGet]
		[Route("/createmessage/{courseId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult CreateMessage(Guid courseId)
		{
			ViewData["pageTitle"] = "Create Course Message";
            ViewData["courseId"] = courseId;


            return View("CreateMessage");
		}

		[HttpPost]
		[Route("/createmessage")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateMessage(EditCourseMessageRequest editCourseMessageRequest)
		{
			CourseResponse response = await _editCourseMessageService.EditCourseMessage(editCourseMessageRequest);

			return RedirectToAction("Course", new { courseId = response.CourseId } );
		}

		// Returns submit assignment view for /submitassignment endpoint
		[HttpGet]
		[Route("/submitassignment/{courseId}")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public IActionResult SubmitAssignment(Guid courseId)
		{
			ViewData["pageTitle"] = "Submit Assignment";
            ViewData["courseId"] = courseId;

            return View("SubmitAssignment");
		}

		[HttpPost]
		[Route("/submitassignment")]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> SubmitAssignment(AssignmentAddRequest assignmentAddRequest, IFormFile assignmentFile)
		{
            // Get the logged in userId
			Guid userId = Guid.Parse(GetUserId());

			assignmentAddRequest.StudentId = userId;

			await _fileService.UploadFile(assignmentFile);		

            // Add Filename to editCourseRequest
            assignmentAddRequest.AssignmentFileName = assignmentFile.FileName;

            AssignmentResponse response = await _assignmentAdderService.AddAssignment(assignmentAddRequest);

			return RedirectToAction("Course", new {courseId = assignmentAddRequest.CourseId});
		}

		// Returns submitted assignments view for /submittedassignments endpoint
		[HttpGet]
		[Route("/submittedassignments/{courseId}")]
		[Authorize(Roles = "Admin,Teacher")]
		public async Task<IActionResult> SubmittedAssignments(Guid courseId, string searchString, string searchBy = "StudentId")
		{
			if (searchString == null)
			{
				searchBy = null;
			}

			List<AssignmentResponse> courseAssignments = new List<AssignmentResponse>();
			List<AssignmentResponse> assignments = await _assignmentGetterService.GetFilterdAssignments(searchBy, searchString);

			foreach (var assignment in assignments)
			{
				if (assignment.CourseId == courseId)
				{
					courseAssignments.Add(assignment);
				}
			}

            ViewData["pageTitle"] = "Submitted Assignments";
            ViewData["courseId"] = courseId;
            ViewData["assignments"] = courseAssignments;

            return View("SubmittedAssignments");
		}

		[HttpPost]
		[Route("/updategrade/{assignmentId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateGrade(UpdateGradeRequest updateGradeRequest)
		{
			AssignmentGradeResponse response = await _updateGradeService.UpdateAssignmentGrade(updateGradeRequest);
			
			return RedirectToAction("SubmittedAssignments", new { courseId = updateGradeRequest.CourseId });
		}
	}
}
