using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Infrastructure.Repositories
{
	public class AssignmentRepository : IAssignmentRepository
	{
		private readonly ApplicationDbContext _db;

		public AssignmentRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public async Task<Assignment> AddAssignment(Assignment assignment)
		{
			_db.Assignments.Add(assignment);
			await _db.SaveChangesAsync();

			return assignment;
		}

		public async Task<Assignment> GetAssignmentByAssignmentId(Guid assignmentId)
		{
			return await _db.Assignments.FirstOrDefaultAsync(assignment => assignment.AssignmentID == assignmentId);
		}

		public async Task<List<Assignment>> GetAssignmentByStudentId(Guid studentId)
		{
			return await _db.Assignments.Where(assignment => assignment.StudentId == studentId).ToListAsync();
		}

		public async Task<List<Assignment>> GetAssignmentsByCourseId(Guid courseId)
		{
			return await _db.Assignments.Where(assignment => assignment.CourseId == courseId).ToListAsync();
		}

		public async Task<Assignment> UpdateAssignmentGrade(Assignment assignment, int grade)
		{
			// Find matching assignment in db
			Assignment? matchingAssignment = await _db.Assignments.FirstOrDefaultAsync(temp => temp.AssignmentID == assignment.AssignmentID);

			// If there is no matching assignment in db return assignment
			if (matchingAssignment == null) return assignment;

			// Update assignment grade
			matchingAssignment.Grade = grade;
			await _db.SaveChangesAsync();

			return matchingAssignment;
		}
	}
}
