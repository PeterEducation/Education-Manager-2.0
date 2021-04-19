using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Repositories
{
    public class CourseEntityConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseEntityConfiguration()
        {
            HasKey(r => r.ID);
            Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
