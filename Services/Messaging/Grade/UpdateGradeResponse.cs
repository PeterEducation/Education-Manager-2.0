using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class UpdateGradeResponse : BaseResponse
    {
        public GradeView GradeView { get; set; }
    }
}
