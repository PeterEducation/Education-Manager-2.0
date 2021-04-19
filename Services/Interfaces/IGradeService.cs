using Services.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IGradeService
    {
        public GetAllGradesResponse GetAll(GetAllGradesRequest getAllPeopleRequest);

        public GetGradeByIdResponse GetById(GetGradeByIdRequest getGradeByIdRequest);

        public CreateGradeResponse Create(CreateGradeRequest createGradeRequest);

        public UpdateGradeResponse Edit(UpdateGradeRequest updateGradeRequest);

        public DeleteGradeResponse Delete(DeleteGradeRequest deleteGradeRequest);
    }
}
