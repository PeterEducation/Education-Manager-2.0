using Services.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ICourseService
    {
        public GetAllCoursesResponse GetAll(GetAllCoursesRequest getAllPeopleRequest);

        public GetCourseByIdResponse GetById(GetCourseByIdRequest getCourseByIdRequest);

        public CreateCourseResponse Create(CreateCourseRequest createCourseRequest);

        public UpdateCourseResponse Edit(UpdateCourseRequest updateCourseRequest);

        public DeleteCourseResponse Delete(DeleteCourseRequest deleteCourseRequest);
    }
}
