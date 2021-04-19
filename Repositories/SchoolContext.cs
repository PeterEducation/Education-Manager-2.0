using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Repositories
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Grade> Grades { get; set; }

        public IDbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GradeEntityConfiguration());
            modelBuilder.Configurations.Add(new CourseEntityConfiguration());
        }
    }
}
