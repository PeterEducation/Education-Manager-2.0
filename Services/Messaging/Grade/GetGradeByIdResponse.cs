using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class GetGradeByIdResponse : BaseResponse
    {
        public GradeView  GradeView { get; set; }
    }
}
