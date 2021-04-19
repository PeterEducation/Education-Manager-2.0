using AutoMapper;
using Models;
using Services.Messaging;
using Services.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    public class GradeMappingProfile : Profile
    {
        public GradeMappingProfile()
        {
            CreateMap<Grade, GradeView>()
                .ForMember(dest => dest.CourseName, src => src.MapFrom(s => s.Course.Name));

            CreateMap<CreateGradeRequest, Grade>()
                .ForMember(dest => dest.ID, src => src.Ignore())
                .ForMember(dest => dest.Course, src => src.Ignore());

            CreateMap<UpdateGradeRequest, Grade>()
                .ForMember(dest => dest.Course, src => src.Ignore());
        }
    }
}
