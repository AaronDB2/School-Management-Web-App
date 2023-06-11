using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Core.DTO;
using SchoolManagementWebApp.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.Services
{
    public class AssignmentGetterService : IAssignmentGetterService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentGetterService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<List<AssignmentResponse>> GetAssignmentsByCourseId(Guid courseId)
        {
            List<Assignment> assignments =  await _assignmentRepository.GetAssignmentsByCourseId(courseId);

            List<AssignmentResponse> response = new List<AssignmentResponse>();

            // Convert all assignments into AssignmentResponse type
            foreach (Assignment assignment in assignments)
            {
                AssignmentResponse assignmentResponse = new AssignmentResponse()
                {
                    AssignmentId = assignment.AssignmentID,
                    StudentId = assignment.StudentId,
                    Grade = assignment.Grade,
                    AssignmentFileName= assignment.AssignmentFileName,
                };
                response.Add(assignmentResponse);
            }

            return response;
        }

		public async Task<List<AssignmentResponse>> GetFilterdAssignments(string searchBy, string? searchString)
		{
			List<Assignment> assignments = searchBy switch
			{
				nameof(AssignmentResponse.StudentId) =>
				 await _assignmentRepository.GetFilterdAssignments(temp =>
				 temp.StudentId.ToString().Contains(searchString)),

				_ => await _assignmentRepository.GetAllAssignments()
			};

			List<AssignmentResponse> response = new List<AssignmentResponse>();

			foreach (Assignment assignment in assignments)
			{
				AssignmentResponse assignmentResponse = new AssignmentResponse()
				{
                    AssignmentFileName = assignment.AssignmentFileName,
                    AssignmentId = assignment.AssignmentID,
                    Grade = assignment.Grade,
                    StudentId = assignment.StudentId,
                    CourseId = assignment.CourseId,
				};

				response.Add(assignmentResponse);
			}

			return response;
		}
	}
}
