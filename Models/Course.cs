using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Course
    {
        public Guid ID { get; set; }

        public int CourseNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Grade> GradeList { get; set; }
    }
}
