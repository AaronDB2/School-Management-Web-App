using FluentAssertions;
using Moq;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using SchoolManagementWebApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.ServiceTests
{
	public class AssignmentServiceTests
	{
		private readonly IAssignmentAdderService _assignmentAdderService;
		private readonly IUpdateGradeService _updateGradeService;
		private readonly IAssignmentGetterService _assignmentGetterService;

		private readonly Mock<IAssignmentRepository> _assignmentsRepositoryMock;
		private readonly IAssignmentRepository _assignmentsRepository;
		public AssignmentServiceTests()
		{
			// Mock AssignmentsRepository
			_assignmentsRepositoryMock = new Mock<IAssignmentRepository>();
			_assignmentsRepository = _assignmentsRepositoryMock.Object;

			// Initialize services
			_assignmentAdderService = new AssignmentAdderService(_assignmentsRepository);
			_updateGradeService = new UpdateGradeService(_assignmentsRepository);
			_assignmentGetterService = new AssignmentGetterService(_assignmentsRepository);
		}

		[Fact]
		public async Task AddAssignment_AssignmentNull_ToBeArgumentNullException()
		{
			//Arrange
			AssignmentAddRequest? assignmentAddRequest = null;

			//Act
			Func<Task> action = async () =>
			{
				await _assignmentAdderService.AddAssignment(assignmentAddRequest);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task AddAssignment_AssignmentDetailComplete_ToBeSuccessfull()
		{
			//Arrange
			AssignmentAddRequest? assignmentAddRequest = new AssignmentAddRequest()
			{
				AssignmentFileName = "TestAssignment.pdf",
				CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				StudentId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
			};

			Assignment assignment = assignmentAddRequest.ToAssignment();
			AssignmentResponse assignmentResponse = new AssignmentResponse() { AssignmentId = assignment.AssignmentID };

			//Mock AddAssignment method from AssignmentsRepository 
			_assignmentsRepositoryMock.Setup
			 (temp => temp.AddAssignment(It.IsAny<Assignment>()))
			 .ReturnsAsync(assignment);

			//Act
			AssignmentResponse assignment_response_from_add = await _assignmentAdderService.AddAssignment(assignmentAddRequest);

			assignmentResponse.AssignmentId = assignment_response_from_add.AssignmentId;

			//Assert
			assignment_response_from_add.AssignmentId.Should().NotBe(Guid.Empty);
			assignment_response_from_add.AssignmentId.Should().Be(assignmentResponse.AssignmentId);

		}

		[Fact]
		public async Task UpdateAssignmentGrade_UpdateGradeRequestIsNull_ToBeArgumentNullException()
		{
			//Arrange
			UpdateGradeRequest? updateGradeRequest = null;

			//Act
			Func<Task> action = async () =>
			{
				await _updateGradeService.UpdateAssignmentGrade(updateGradeRequest);
			};

			//Assert
			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task UpdateAssignmentGrade_UpdateGradeRequestDetailsComplete_ToBeSuccessfull()
		{
			//Arrange
			UpdateGradeRequest? updateGradeRequest = new UpdateGradeRequest() 
			{ 
				AssignmentId = Guid.Parse("DA641EE7-004A-4543-8402-E5E897349FF5"), 
				Grade = 10 
			};

			// TODO: Fill in details for testing
			Assignment assignment = new Assignment()
			{
				AssignmentID = Guid.Parse("DA641EE7-004A-4543-8402-E5E897349FF5"),
				Grade = 10,
				AssignmentFileName = "TestAssignment.pdf",
				CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				StudentId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
			};

			//Mock GetAssignmentByAssignmentId method from AssignmentsRepository 
			_assignmentsRepositoryMock.Setup
			 (temp => temp.GetAssignmentByAssignmentId(It.IsAny<Guid>()))
			 .ReturnsAsync(assignment);

			//Mock UpdateAssignmentGrade method from AssignmentsRepository 
			_assignmentsRepositoryMock.Setup
			 (temp => temp.UpdateAssignmentGrade(It.IsAny<Assignment>(), It.IsAny<int>()))
			 .ReturnsAsync(assignment);

			//Act
			AssignmentGradeResponse response = await _updateGradeService.UpdateAssignmentGrade(updateGradeRequest);

			//Assert
			response.Grade.Should().Be(10);
		}

		[Fact]
		public async Task GetFilterdAssignments_SearchByStudentId_ToBeSuccessfull()
		{
			//Arrange
			Assignment assignment = new Assignment()
			{
				AssignmentFileName = "Test.pdf",
				AssignmentID = Guid.NewGuid(),
				CourseId = Guid.NewGuid(),
				StudentId = Guid.NewGuid(),
				Grade = 0
			};

			List<Assignment> assignments = new List<Assignment>() { assignment };

			//Mock GetFilterdAssignments method from AssignmentRepository 
			_assignmentsRepositoryMock.Setup
			 (temp => temp.GetFilterdAssignments(It.IsAny<Expression<Func<Assignment, bool>>>()))
			 .ReturnsAsync(assignments);

			//Act
			List<AssignmentResponse> response = await _assignmentGetterService.GetFilterdAssignments("StudentId", assignment.CourseId.ToString());

			//Assert
			response.Should().NotBeNull();
			response[0].CourseId.Should().Be(assignment.CourseId);
		}
	}
}
