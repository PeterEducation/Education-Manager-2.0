using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class CourseRepository : IRepository<Course>, ICourseRepository
    {
        private readonly SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            _context = context;
        }

        public void Create(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Delete(Guid courseId)
        {
            Course course = _context.Courses.Find(courseId);
            _context.Courses.Remove(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public IQueryable<Course> GetAll(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return _context.Courses;
            }
            else
            {
                return _context.Courses.Where(p => p.Name.Contains(searchString) || p.Name.Contains(searchString));
            }
        }

        public Course GetById(Guid courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public void Update(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
        }
    }
}
