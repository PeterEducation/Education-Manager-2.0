using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Views
{
    public class GradeView
    {
        public Guid ID { get; set; }

        public Guid CourseID { get; set; }

        public int GradeNumber { get; set; }

        public double Value { get; set; }

        public string Note { get; set; }

        public string CourseName { get; set; }
    }
}
