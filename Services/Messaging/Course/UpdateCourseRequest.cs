using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class UpdateCourseRequest
    {
        public Guid ID { get; set; }

        public int CourseNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
