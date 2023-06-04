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
                };
                response.Add(assignmentResponse);
            }

            return response;
        }
    }
}
