using Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationManager2.Models
{
    public class GradeIndexViewModel
    {
        public List<GradeView> GradeViews { get; set; }

        public string SearchString { get; set; }
    }
}
