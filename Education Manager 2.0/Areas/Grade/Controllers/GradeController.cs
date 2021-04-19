using Microsoft.AspNetCore.Mvc;
using EducationManager2.Models;
using Repositories;
using Services;
using Services.Messaging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EducationManager2.Controllers
{
    [Area("Grade")]
    [Route("[area]/[controller]/[action]")]
    public class GradeController : Controller
    {
        private IGradeService _gradeService;
        private ICourseService _courseService;

        public GradeController(IGradeService gradeService, ICourseService courseService)
        {
            _gradeService = gradeService;
            _courseService = courseService;
        }

        // GET: /Grades/
        public IActionResult Index()
        {
            var getAllGradesResponse = _gradeService.GetAll(new GetAllGradesRequest());
            if (getAllGradesResponse.IsSuccess == true)
            {
                return View(
                    "Index",
                    new GradeIndexViewModel()
                    {
                        GradeViews = getAllGradesResponse.GradeViews,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Loading Error");
                return View("Index");
            }
        }

        // [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(GradeIndexViewModel gradeIndexViewModel)
        {
            var getAllGradesResponse = _gradeService.GetAll(new GetAllGradesRequest { SearchString = gradeIndexViewModel.SearchString });
            if (getAllGradesResponse.IsSuccess == true)
            {
                return View(
                    "Index",
                    new GradeIndexViewModel()
                    {
                        GradeViews = getAllGradesResponse.GradeViews,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Loading Error");
                return View("Index");
            }
        }

        // GET: /Course/Details/5
        public IActionResult Details(Guid id)
        {
            var getGradeByIdResponse = _gradeService.GetById(new GetGradeByIdRequest { ID = id });

            if (getGradeByIdResponse.IsSuccess == true)
            {
                return View(
                    new GradeDetailsViewModel()
                    {
                        GradeView = getGradeByIdResponse.GradeView,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Get Grade Error");
                return RedirectToAction("Index");
            }
        }

        public IActionResult Create()
        {
            GradeCreateViewModel gradeCreateViewModel = new GradeCreateViewModel();
            gradeCreateViewModel.SelectListItems = PopulateCoursesDropDownList();
            return View("Create", gradeCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GradeCreateViewModel gradeCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var createGradeResponse = _gradeService.Create(new CreateGradeRequest
                {
                    CourseID = gradeCreateViewModel.GradeView.CourseID,
                    GradeNumber = gradeCreateViewModel.GradeView.GradeNumber,
                    Value = gradeCreateViewModel.GradeView.Value,
                    Note = gradeCreateViewModel.GradeView.Note,
                });

                if (createGradeResponse.IsSuccess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Create Grade Error");
                    gradeCreateViewModel.SelectListItems = PopulateCoursesDropDownList();
                    return View("Create", gradeCreateViewModel);
                }
            }
            else
            {
                gradeCreateViewModel.SelectListItems = PopulateCoursesDropDownList();
                ModelState.AddModelError(string.Empty, "Invalid Input Error");
                return View("Create", gradeCreateViewModel);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var getGradeByIdResponse = _gradeService.GetById(new GetGradeByIdRequest { ID = id });
            if (getGradeByIdResponse.IsSuccess == true)
            {
                return View(
                        new GradeEditViewModel()
                        {
                            GradeView = getGradeByIdResponse.GradeView,
                            SelectListItems = PopulateCoursesDropDownList(),
                        });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Loading Grade Error");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GradeEditViewModel gradeEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var updateGradeResponse = _gradeService.Edit(new UpdateGradeRequest
                {
                    ID = gradeEditViewModel.GradeView.ID,
                    CourseID = gradeEditViewModel.GradeView.CourseID,
                    GradeNumber = gradeEditViewModel.GradeView.GradeNumber,
                    Value = gradeEditViewModel.GradeView.Value,
                    Note = gradeEditViewModel.GradeView.Note,
                });
                if (updateGradeResponse.IsSuccess == true)
                {
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Loading Error ;)");
                    gradeEditViewModel.SelectListItems = PopulateCoursesDropDownList();
                    return View("Edit", gradeEditViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Input Error");
                gradeEditViewModel.SelectListItems = PopulateCoursesDropDownList();
                return View("Edit", gradeEditViewModel);
            }
        }

        public IActionResult Delete(Guid id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ModelState.AddModelError(string.Empty, "Delete Grade Error");
            }

            var getGradeByIdResponse = _gradeService.GetById(new GetGradeByIdRequest { ID = id });
            if (getGradeByIdResponse.IsSuccess == true)
            {
                return View(
                    new GradeDeleteViewModel()
                    {
                        GradeView = getGradeByIdResponse.GradeView,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Delete Grade Error");
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var deleteGradeResponse = _gradeService.Delete(new DeleteGradeRequest { ID = id });
            if (deleteGradeResponse.IsSuccess == true)
            {
                return View("Index");
            }
            else
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> PopulateCoursesDropDownList()
        {
            var courses = (from d in _courseService.GetAll(new GetAllCoursesRequest()).CourseViews
                               orderby d.Name
                               select d).ToList();

            var selectListItems = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            courses.ForEach(d => selectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(d.Name, d.ID.ToString())));
            return selectListItems;
        }
    }
}
