using AutoMapper;
using Models;
using Services.Messaging;
using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, CourseView>();

            CreateMap<CreateCourseRequest, Course>()
                .ForMember(dest => dest.GradeList, src => src.Ignore())
                .ForMember(dest => dest.ID, src => src.Ignore());

            CreateMap<UpdateCourseRequest, Course>()
                .ForMember(dest => dest.GradeList, src => src.Ignore());
        }
    }
}
