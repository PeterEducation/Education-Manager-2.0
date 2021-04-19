using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class GradeRepository : IRepository<Grade>, IGradeRepository
    {
        private readonly SchoolContext _context;

        public GradeRepository(SchoolContext context)
        {
            _context = context;
        }

        public void Create(Grade grade)
        {
            _context.Grades.Add(grade);
        }

        public void Delete(Guid gradeId)
        {
            Grade grade = _context.Grades.Find(gradeId);
            _context.Grades.Remove(grade);
        }

        public void Delete(Grade grade)
        {
            _context.Grades.Remove(grade);
        }

        public IQueryable<Grade> GetAll(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return _context.Grades.Include(g => g.Course);
            }
            else
            {
                return _context.Grades.Include(g => g.Course).Where(g => g.Note.Contains(searchString) || g.Course.Name.Contains(searchString));
            }
        }

        public Grade GetById(Guid gradeId)
        {
            return _context.Grades.Find(gradeId);
        }

        public void Update(Grade grade)
        {
            _context.Entry(grade).State = EntityState.Modified;
        }
    }
}
