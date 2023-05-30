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
	public class UpdateGradeService : IUpdateGradeService
	{
		private readonly IAssignmentRepository _assignmentRepository;

		public UpdateGradeService(IAssignmentRepository assignmentRepository)
		{
			_assignmentRepository = assignmentRepository;
		}

		public async Task<AssignmentGradeResponse> UpdateAssignmentGrade(UpdateGradeRequest updateGradeRequest)
		{
			// Check if updateGradeRequest is null
			if (updateGradeRequest == null) 
			{
				throw new ArgumentNullException(nameof(updateGradeRequest));
			}


			// Get the assignment from data store based on assignment id
			Assignment assignment = await _assignmentRepository.GetAssignmentByAssignmentId(updateGradeRequest.AssignmentId);

			//TODO check if assignment exists

			// Update assignment
			Assignment updatedAssignment = await _assignmentRepository.UpdateAssignmentGrade(assignment, updateGradeRequest.Grade);

			AssignmentGradeResponse assignmentGradeResponse = new AssignmentGradeResponse() 
			{
				AssignmentId = updatedAssignment.AssignmentID,
				Grade = updatedAssignment.Grade,
			};

			return assignmentGradeResponse;
		}
	}
}
