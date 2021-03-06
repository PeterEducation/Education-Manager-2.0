using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Grade
    {
        public Guid ID { get; set; }

        public Guid CourseID { get; set; }

        public int GradeNumber { get; set; }

        public double Value { get; set; }

        public string Note { get; set; }

        public virtual Course Course { get; set; }
    }
}
