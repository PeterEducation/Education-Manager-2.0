using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class GetAllCoursesResponse : BaseResponse
    {
        public List<CourseView> CourseViews { get; set; }
    }
}
