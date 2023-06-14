using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Infrastructure.DbContext;
using SchoolManagementWebApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.RepositoryTests
{
	public class AssignmentRepositoryTests
	{
		private readonly AssignmentRepository _assignmentRepository;

		// Initial data store data
		private List<Assignment> assignmentInitialData = new List<Assignment>() { new Assignment
		{
			AssignmentID = new Guid("9E5EC7DB-F1EA-4921-B77B-04CE9DE6CF9A"),
			AssignmentFileName = "TestAssignment.pdf",
			CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
			StudentId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
		}, new Assignment
		{
			AssignmentID = new Guid("E78C8E1B-1DB0-410E-BB49-13EE428852E2"),
			AssignmentFileName = "TestAssignment.pdf",
			CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
			StudentId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
		}};

		/// <summary>
		/// Constructor for initializing private variables and mocking
		/// </summary>
		public AssignmentRepositoryTests()
		{
			// Mock the dbContext
			DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
			var dbContext = dbContextMock.Object;

			// Mock db sets
			dbContextMock.CreateDbSetMock(temp => temp.Assignments, assignmentInitialData);

			_assignmentRepository = new AssignmentRepository(dbContext);
		}

		[Fact]
		public async Task AddAssignment_ShouldAddNewAssignmentObjectToDataStore()
		{
			var addedAssignment = await _assignmentRepository.AddAssignment(new Assignment
			{
				AssignmentID = new Guid("DA641EE7-004A-4543-8402-E5E897349FF5"),
				AssignmentFileName = "TestAssignment.pdf",
				CourseId = Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"),
				StudentId = Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"),
			});

			// TODO i am calling a second method here so this test is technically not valid as i test a second method here aswell. Should changes it later
			var returnedAssignment = await _assignmentRepository.GetAssignmentByAssignmentId(Guid.Parse("DA641EE7-004A-4543-8402-E5E897349FF5"));

			Assert.Equal(returnedAssignment, addedAssignment);
		}

        [Fact]
        public async Task GetAllAssignments_ShouldReturnAllAssignmentsFromDataStore()
        {
            var returnedAssignments = await _assignmentRepository.GetAllAssignments();

            // Check if returnedAssignments is of IEnumerable type
            Assert.IsAssignableFrom<IEnumerable<Assignment>>(returnedAssignments);

            // Check if returnedAssignments and assignmentsInitialData are the same size
            Assert.Equal(returnedAssignments.Count, assignmentInitialData.Count);

            // Check if returnedAssignments and assignmentInitialData first assignment in collection have the same Id
            Assert.Equal(returnedAssignments[0].AssignmentID, assignmentInitialData[0].AssignmentID);
        }

        [Fact]
		public async Task GetAssignmentByAssignmentId_ShouldReturnAssignmentWithCorrectAssignmentIdFromDataStore()
		{
			var returnedAssignment = await _assignmentRepository.GetAssignmentByAssignmentId(Guid.Parse("9E5EC7DB-F1EA-4921-B77B-04CE9DE6CF9A"));

			Assert.Equal(returnedAssignment.AssignmentID, Guid.Parse("9E5EC7DB-F1EA-4921-B77B-04CE9DE6CF9A"));
		}

		[Fact]
		public async Task GetAssignmentByStudentId_ShouldReturnAssignmentsWithCorrectStudentIdFromDataStore()
		{
			var returnedAssignments = await _assignmentRepository.GetAssignmentByStudentId(Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"));

			// Check if returnedAssignments is of IEnumerable type
			Assert.IsAssignableFrom<IEnumerable<Assignment>>(returnedAssignments);

			// Check if returnedAssignments and assignmentInitialData are the same size
			Assert.Equal(returnedAssignments.Count, assignmentInitialData.Count);

			// Check if returnedAssignments and assignmentInitialData first assignment in collection have the same studentId
			Assert.Equal(returnedAssignments[0].StudentId, Guid.Parse("D0A86355-484E-48E0-89E5-68735CE5EC3C"));
		}

		[Fact]
		public async Task GetAssignmentsByCourseId_ShouldReturnAssignmentsWithCorrectCourseIdFromDataStore()
		{
			var returnedAssignments = await _assignmentRepository.GetAssignmentsByCourseId(Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"));

			// Check if returnedAssignments is of IEnumerable type
			Assert.IsAssignableFrom<IEnumerable<Assignment>>(returnedAssignments);

			// Check if returnedAssignments and assignmentInitialData are the same size
			Assert.Equal(returnedAssignments.Count, assignmentInitialData.Count);

			// Check if returnedAssignments and assignmentInitialData first assignment in collection have the same courseId
			Assert.Equal(returnedAssignments[0].CourseId, Guid.Parse("E5376ECE-7E42-4604-A3A2-23D69383E8F2"));
		}

		[Fact]
		public async Task UpdateAssignmentGrade_ShoudUpdateGradeForASingleEntityInDataStore()
		{
			Assignment assignmentToBeUpdated = assignmentInitialData[0];

			var returnedAssignment = await _assignmentRepository.UpdateAssignmentGrade(assignmentToBeUpdated, 10);

			// Check if returnedAssignment has the updated grade
			Assert.Equal(returnedAssignment.Grade, 10);
		}


	}
}
