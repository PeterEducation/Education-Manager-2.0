using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Repositories
{
    public class GradeEntityConfiguration : EntityTypeConfiguration<Grade>
    {
        public GradeEntityConfiguration()
        {
            HasKey(r => r.ID);
            Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Course).WithMany(d => d.GradeList).WillCascadeOnDelete(false);
        }
    }
}
