using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class GetCourseByIdResponse : BaseResponse
    {
        public CourseView CourseView { get; set; }
    }
}
