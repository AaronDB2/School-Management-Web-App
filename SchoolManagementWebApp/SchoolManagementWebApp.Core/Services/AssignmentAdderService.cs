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
	public class AssignmentAdderService : IAssignmentAdderService
	{
		private readonly IAssignmentRepository _assignmentRepository;

		public AssignmentAdderService(IAssignmentRepository assignmentRepository)
		{
			_assignmentRepository = assignmentRepository;
		}

		public async Task<AssignmentResponse> AddAssignment(AssignmentAddRequest? assignmentAddRequest)
		{
			//AssignmentAddRequest parameter can't be null
			if (assignmentAddRequest == null)
			{
				throw new ArgumentNullException(nameof(assignmentAddRequest));
			}

			//TODO: Check teacher Id valid

			//TODO: Check course Id valid

			//Convert object from AssignmentAddRequest to Assignment type
			Assignment assignment = assignmentAddRequest.ToAssignment();

			//generate AssignmentId
			assignment.AssignmentID = Guid.NewGuid();

			//Add assignment object into _assignmentRepository
			await _assignmentRepository.AddAssignment(assignment);

			// Generate AssignmentResponse
			AssignmentResponse assignmentResponse = new AssignmentResponse() { AssignmentId = assignment.AssignmentID };

			return assignmentResponse;
		}
	}
}
