using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Messaging
{
    public class GetAllGradesResponse : BaseResponse
    {
        public List<GradeView> GradeViews { get; set; }
    }
}
