using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        private readonly IGradeRepository _gradeRepository;
        private readonly ICourseRepository _courseRepository;
        private bool _disposed = false;

        public UnitOfWork(SchoolContext context, IGradeRepository gradeRepository, ICourseRepository courseRepository)
        {
            _context = context;
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
        }

        public IGradeRepository GradeRepository
        {
            get
            {
                return _gradeRepository;
            }
        }

        public ICourseRepository CourseRepository
        {
            get
            {
                return _courseRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ---------------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
