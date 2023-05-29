﻿using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Core.Domain.Entities;
using SchoolManagementWebApp.Core.Domain.IdentityEntities;
using SchoolManagementWebApp.Core.Domain.RepositoryContracts;
using SchoolManagementWebApp.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Infrastructure.Repositories
{
	public class CoursesRepository : ICoursesRepository
	{
		private readonly ApplicationDbContext _db;

		public CoursesRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<Course> AddCourse(Course course)
		{
			_db.Courses.Add(course);
			await _db.SaveChangesAsync();

			return course;
		}


		// Usermanager?
		public Task<Course> EnrollInCourse(Course course, ApplicationUser user)
		{
			// Find matching course
			//Course? matchingCourse = await _db.Courses.FirstOrDefaultAsync(temp => temp.CourseId == course.CourseId);
			//// TODO: Usermanager logic for finding matching user

			//if (matchingCourse == null)
			//{
			//	return course;
			//}

			//matchingCourse.Students.Add(user);
			//await _db.SaveChangesAsync();
			//return matchingCourse;

			throw new NotImplementedException();
		}

		public async Task<List<Course>> GetAllCourses()
		{
			return await _db.Courses.ToListAsync();

		}

		public async Task<Course?> GetCourseByCourseId(Guid courseId)
		{
			return await _db.Courses.FirstOrDefaultAsync(course => course.CourseId == courseId);
		}

		public async Task<List<Course>> GetFilterdCourses(Expression<Func<Course, bool>> predicate)
		{
			return await _db.Courses.Include(temp => temp.Students).Include(temp => temp.Assignments)
			.Where(predicate)
			.ToListAsync();
		}

		public async Task<Course> UpdateCourseInfo(Course course)
		{
			// Find matching course in db
			Course? matchingCourse = await _db.Courses.FirstOrDefaultAsync(temp => temp.CourseId == course.CourseId);

			// If there is no matching course in db return course
			if (matchingCourse == null) return course;

			// Update courseText and CourseFileName
			matchingCourse.CourseText = course.CourseText;
			matchingCourse.CourseFileName = course.CourseFileName;

			await _db.SaveChangesAsync();

			return matchingCourse;
		}

		public async Task<Course> UpdateCourseMessage(Course course)
		{
			// Find matching course in db
			Course? matchingCourse = await _db.Courses.FirstOrDefaultAsync(temp => temp.CourseId == course.CourseId);

			// If there is no matching course in db return course
			if (matchingCourse == null) return course;

			// Update courseMessage
			matchingCourse.Message = course.Message;

			await _db.SaveChangesAsync();

			return matchingCourse;
		}
	}
}