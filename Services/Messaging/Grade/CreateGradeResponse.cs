using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class CreateGradeResponse : BaseResponse
    {
        public GradeView GradeView { get; set; }
    }
}
