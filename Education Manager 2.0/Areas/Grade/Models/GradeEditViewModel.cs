using Models;
using Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationManager2.Models
{
    public class GradeEditViewModel
    {
        public GradeView GradeView { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> SelectListItems { get; set; }
    }
}
