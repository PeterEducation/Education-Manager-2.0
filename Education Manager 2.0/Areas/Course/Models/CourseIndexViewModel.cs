using Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationManager2.Models
{
    public class CourseIndexViewModel
    {
        public List<CourseView> CourseViews { get; set; }
        public string SearchString { get; set; }
    }
}
