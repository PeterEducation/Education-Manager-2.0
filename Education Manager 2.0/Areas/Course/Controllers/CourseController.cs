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
    [Area("Course")]
    [Route("[area]/[controller]/[action]")]
    public class CourseController : Controller
    {
        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var getAllCoursesResponse = _courseService.GetAll(new GetAllCoursesRequest());
            if (getAllCoursesResponse.IsSuccess == true)
            {
                return View(
                    "Index",
                    new CourseIndexViewModel()
                    {
                        CourseViews = getAllCoursesResponse.CourseViews,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Loading Error");
                return View("Index");
            }
        }

        [ValidateAntiForgeryToken]
        public IActionResult Search(CourseIndexViewModel courseIndexViewModel)
        {
            var getAllCoursesResponse = _courseService.GetAll(new GetAllCoursesRequest { SearchString = courseIndexViewModel.SearchString });
            if (getAllCoursesResponse.IsSuccess == true)
            {
                return View(
                    "Index",
                    new CourseIndexViewModel()
                    {
                        CourseViews = getAllCoursesResponse.CourseViews,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Loading Error");
                return View("Index");
            }
        }

        public IActionResult Details(Guid id)
        {
            var getCourseByIdResponse = _courseService.GetById(new GetCourseByIdRequest { ID = id });

            if (getCourseByIdResponse.IsSuccess == true)
            {
                return View(
                    new CourseDetailsViewModel()
                    {
                        CourseView = getCourseByIdResponse.CourseView,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Get Course Error");
                return RedirectToAction("Index");
            }
        }

        public IActionResult Create()
        {
            CourseCreateViewModel courseCreateViewModel = new CourseCreateViewModel();
            return View("Create", courseCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseCreateViewModel courseCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var createCourseResponse = _courseService.Create(new CreateCourseRequest
                {
                    CourseNumber = courseCreateViewModel.CourseView.CourseNumber,
                    Name = courseCreateViewModel.CourseView.Name,
                    Description = courseCreateViewModel.CourseView.Description,
                });
                if (createCourseResponse.IsSuccess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Create Course Error");
                    return View("Create", courseCreateViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Course Error");
                return View("Create", courseCreateViewModel);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var getCourseByIdResponse = _courseService.GetById(new GetCourseByIdRequest { ID = id });
            if (getCourseByIdResponse.IsSuccess == true)
            {
                return View(
                        new CourseEditViewModel()
                        {
                            CourseView = getCourseByIdResponse.CourseView,
                        });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Loading Course Error");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseEditViewModel courseEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var updateCourseResponse = _courseService.Edit(new UpdateCourseRequest
                {
                    ID = courseEditViewModel.CourseView.ID,
                    CourseNumber = courseEditViewModel.CourseView.CourseNumber,
                    Name = courseEditViewModel.CourseView.Name,
                    Description = courseEditViewModel.CourseView.Description,
                });
                if (updateCourseResponse.IsSuccess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Loading Error ;)");
                    return View("Edit", courseEditViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Input Error");
                return View("Edit", courseEditViewModel);
            }
        }

        public IActionResult Delete(Guid id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ModelState.AddModelError(string.Empty, "Delete Departemt Error");
            }

            var getCourseByIdResponse = _courseService.GetById(new GetCourseByIdRequest { ID = id });
            if (getCourseByIdResponse.IsSuccess == true)
            {
                return View(
                    new CourseDeleteViewModel()
                    {
                        CourseView = getCourseByIdResponse.CourseView,
                    });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Delete Departemt Error");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                CourseDeleteViewModel courseDeleteViewModel = new CourseDeleteViewModel();
                courseDeleteViewModel.CourseView = _courseService.GetById(new GetCourseByIdRequest { ID = id }).CourseView;
                _courseService.Delete(new DeleteCourseRequest { ID = id });
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }
    }
}
