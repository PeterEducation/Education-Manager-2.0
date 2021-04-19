using AutoMapper;
using Models;
using Repositories;
using Services.Messaging;
using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: /Courses/
        public GetAllCoursesResponse GetAll(GetAllCoursesRequest getAllCoursesRequest)
        {
            var courses = _unitOfWork.CourseRepository.GetAll(getAllCoursesRequest.SearchString);
            if (courses == null)
            {
                return new GetAllCoursesResponse()
                {
                    IsSuccess = false,
                };
            }
            else
            {
                return new GetAllCoursesResponse()
                {
                    CourseViews = _mapper.Map<List<CourseView>>(courses.ToList()),
                    IsSuccess = true,
                };
            }
        }

        // GET: /Course/Details/5
        public GetCourseByIdResponse GetById(GetCourseByIdRequest getCourseByIdRequest)
        {
            try
            {
                var course = _unitOfWork.CourseRepository.GetById(getCourseByIdRequest.ID);
                if (course == null)
                {
                    return new GetCourseByIdResponse()
                    {
                        IsSuccess = false,
                    };
                }

                var courseView = _mapper.Map<CourseView>(course);
                return new GetCourseByIdResponse()
                {
                    CourseView = courseView,
                    IsSuccess = true,
                };
            }
            catch
            {
                return new GetCourseByIdResponse()
                {
                    IsSuccess = false,
                };
            }
        }

        public CreateCourseResponse Create(CreateCourseRequest createCourseRequest)
        {
            try
            {
                var course = _mapper.Map<Course>(createCourseRequest);
                if (course == null)
                {
                    return new CreateCourseResponse()
                    {
                        IsSuccess = false,
                    };
                }

                _unitOfWork.CourseRepository.Create(course);
                _unitOfWork.Save();

                return new CreateCourseResponse()
                {
                    IsSuccess = true,
                };
            }
            catch
            {
                return new CreateCourseResponse()
                {
                    IsSuccess = false,
                };
            }
        }

        public UpdateCourseResponse Edit(UpdateCourseRequest updateCourseRequest)
        {
            try
            {
                var currentCourse = _unitOfWork.CourseRepository.GetById(updateCourseRequest.ID);
                if (currentCourse == null)
                {
                    return new UpdateCourseResponse()
                    {
                        IsSuccess = false,
                    };
                }

                var newCourse = _mapper.Map(updateCourseRequest, currentCourse);
                _unitOfWork.CourseRepository.Update(newCourse);

                _unitOfWork.Save();

                var courseView = _mapper.Map<CourseView>(newCourse);

                return new UpdateCourseResponse()
                {
                    IsSuccess = true,
                    CourseView = courseView,
                };
            }
            catch
            {
                return new UpdateCourseResponse()
                {
                    IsSuccess = false,
                };
            }
        }

        public DeleteCourseResponse Delete(DeleteCourseRequest deleteCourseRequest)
        {
            try
            {
                var course = _unitOfWork.CourseRepository.GetById(deleteCourseRequest.ID);

                if (course == null)
                {
                    return new DeleteCourseResponse()
                    {
                        IsSuccess = false,
                    };
                }

                _unitOfWork.CourseRepository.Delete(course);
                _unitOfWork.Save();

                return new DeleteCourseResponse()
                {
                    IsSuccess = true,
                };
            }
            catch
            {
                return new DeleteCourseResponse()
                {
                    IsSuccess = false,
                };
            }
        }
    }
}
