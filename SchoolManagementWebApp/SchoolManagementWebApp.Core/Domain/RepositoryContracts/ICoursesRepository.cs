
using SchoolManagementWebApp.Core.Domain.Entities;
using System.Linq.Expressions;
using System;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;

namespace SchoolManagementWebApp.Core.Domain.RepositoryContracts
{
	/// <summary>
	/// Interface that represents data access logic for managing Course entity
	/// </summary>
	public interface ICoursesRepository
	{
		/// <summary>
		/// Returns the course from the data store that matched the searched courseId
		/// </summary>
		/// <param name="courseId">CourseId (guid) to search</param>
		/// <returns>Course that matched the given courseId or null</returns>
		Task<Course?> GetCourseByCourseId(Guid courseId);

		/// <summary>
		/// Returns all course objects from data store based on given expression
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>List of course objects from data store that match the given expression</returns>
		Task<List<Course>> GetFilterdCourses(Expression<Func<Course, bool>> predicate);

		/// <summary>
		/// Returns all courses from the data store
		/// </summary>
		/// <returns>List of all the courses from the data store</returns>
		Task<List<Course>> GetAllCourses();

		/// <summary>
		/// Adds a new course object to the data store
		/// </summary>
		/// <param name="course">Course object to add</param>
		/// <returns>The course object that was added to the data store</returns>
		Task<Course> AddCourse(Course course);

		/// <summary>
		/// Enrolls the user into the course. Updates the join table between applicationUser entity and Course entity in data store
		/// </summary>
		/// <param name="course">Course object to enroll in</param>
		/// <param name="user">ApplicationUser object that enrolls</param>
		/// <returns>Enrolled course</returns>
		Task<Course> EnrollInCourse(Course course, ApplicationUser user);

		/// <summary>
		/// Update course data store information. It will update the course file name and course text.
		/// </summary>
		/// <param name="course">Course object to update</param>
		/// <returns>Updated course object</returns>
		Task<Course> UpdateCourseInfo(Course course);

		/// <summary>
		/// Updates course data store information. It will update the course message.
		/// </summary>
		/// <param name="course">Course object to update</param>
		/// <returns>Updated course object</returns>
		Task<Course> UpdateCourseMessage(Course course);
	}
}
