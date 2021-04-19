using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class CreateCourseRequest
    {
        public int CourseNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
