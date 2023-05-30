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
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.ServiceTests
{
	public class AssignmentServiceTests
	{
		private readonly IAssignmentAdderService _assignmentAdderService;

		private readonly Mock<IAssignmentRepository> _assignmentsRepositoryMock;
		private readonly IAssignmentRepository _assignmentsRepository;
		public AssignmentServiceTests()
		{
			// Mock AssignmentsRepository
			_assignmentsRepositoryMock = new Mock<IAssignmentRepository>();
			_assignmentsRepository = _assignmentsRepositoryMock.Object;

			// Initialize services
			_assignmentAdderService = new AssignmentAdderService(_assignmentsRepository);
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
		public async Task AddAssignment_AssignmentDetailComplete_ToBeSuccessful()
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
	}
}
