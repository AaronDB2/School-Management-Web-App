﻿using SchoolManagementWebApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace SchoolManagementWebApp.Core.Domain.RepositoryContracts
{
	public interface IAssignmentRepository
	{
		/// <summary>
		/// Gets assignment from data store that matches the assignment id
		/// </summary>
		/// <param name="assignment">assignment id to search</param>
		/// <returns>Assignment object from data store that matches the assignment id</returns>
		Task<Assignment> GetAssignmentByAssignmentId(Guid assignment);

		/// <summary>
		/// Gets the assignments from the data store that matches the given student id
		/// </summary>
		/// <param name="studentId">Student id (guid) to search</param>
		/// <returns>List of assignment objects that matches the given student id</returns>
		Task<List<Assignment>> GetAssignmentByStudentId(Guid studentId);

		/// <summary>
		/// Gets the assignments from the data store that matches the given course id
		/// </summary>
		/// <param name="courseId">Course id (guid) to search</param>
		/// <returns>List of assignment objects that matches the given course id</returns>
		Task<List<Assignment>> GetAssignmentsByCourseId(Guid courseId);

		/// <summary>
		/// Adds a new assignment object to the data store
		/// </summary>
		/// <param name="assignment">Assignment object to add</param>
		/// <returns>The assignment object that was added to the data store</returns>
		Task<Assignment> AddAssignment(Assignment assignment);

		/// <summary>
		/// Update assignment data store information. It will update the assignment grade
		/// </summary>
		/// <param name="assignment">Assignment object to update</param>
		/// <returns>Updated assignment object</returns>
		Task<Assignment> UpdateAssignmentGrade(Assignment assignment, int grade);
	}
}
