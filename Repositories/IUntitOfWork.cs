using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGradeRepository GradeRepository { get; }

        ICourseRepository CourseRepository { get; }

        void Save();
    }
}
