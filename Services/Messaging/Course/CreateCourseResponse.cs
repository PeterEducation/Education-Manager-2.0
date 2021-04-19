using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class CreateCourseResponse : BaseResponse
    {
        public CourseView CourseView { get; set; }
    }
}
