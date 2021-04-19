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
    public class GradeService : IGradeService
    {
        private IUnitOfWork _unitOfWork;

        private IMapper _mapper;

        public GradeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: /Grades/
        public GetAllGradesResponse GetAll(GetAllGradesRequest getAllGradesRequest)
        {
            var grades = _unitOfWork.GradeRepository.GetAll(getAllGradesRequest.SearchString);
            if (grades == null)
            {
                return new GetAllGradesResponse()
                {
                    IsSuccess = false,
                };
            }
            else
            {
                return new GetAllGradesResponse()
                {
                    GradeViews = _mapper.Map<List<GradeView>>(grades.ToList()),
                    IsSuccess = true,
                };
            }
        }

        // GET: /Course/Details/5
        public GetGradeByIdResponse GetById(GetGradeByIdRequest getGradeByIdRequest)
        {
            try
            {
                var grade = _unitOfWork.GradeRepository.GetById(getGradeByIdRequest.ID);
                if (grade == null)
                {
                    return new GetGradeByIdResponse()
                    {
                        IsSuccess = false,
                    };
                }

                var gradeView = _mapper.Map<GradeView>(grade);
                return new GetGradeByIdResponse()
                {
                    GradeView = gradeView,
                    IsSuccess = true,
                };
            }
            catch
            {
                return new GetGradeByIdResponse()
                {
                    IsSuccess = false,
                };
            }
        }

        public CreateGradeResponse Create(CreateGradeRequest createGradeRequest)
        {
            try
            {
                var grade = _mapper.Map<Grade>(createGradeRequest);
                if (grade == null)
                {
                    return new CreateGradeResponse()
                    {
                        IsSuccess = false,
                    };
                }


                _unitOfWork.GradeRepository.Create(grade);
                _unitOfWork.Save();

                return new CreateGradeResponse()
                {
                    IsSuccess = true,
                };
            }
            catch
            {
                return new CreateGradeResponse()
                {
                    IsSuccess = false,
                };
            }
        }

        public UpdateGradeResponse Edit(UpdateGradeRequest updateGradeRequest)
        {
            try
            {
                Grade currentGrade = _unitOfWork.GradeRepository.GetById(updateGradeRequest.ID);
                if (currentGrade == null)
                {
                    return new UpdateGradeResponse
                    {
                        IsSuccess = false,
                    };
                }

                var newGrade = _mapper.Map(updateGradeRequest, currentGrade);
                _unitOfWork.GradeRepository.Update(newGrade);

                _unitOfWork.Save();

                var gradeView = _mapper.Map<GradeView>(newGrade);

                return new UpdateGradeResponse
                {
                    IsSuccess = true,
                    GradeView = gradeView,
                };
            }
            catch
            {
                return new UpdateGradeResponse()
                {
                    IsSuccess = false,
                };
            }
        }

        public DeleteGradeResponse Delete(DeleteGradeRequest deleteGradeRequest)
        {
            try
            {
                var grade = _unitOfWork.GradeRepository.GetById(deleteGradeRequest.ID);

                if (grade == null)
                {
                    return new DeleteGradeResponse()
                    {
                        IsSuccess = false,
                    };
                }

                _unitOfWork.GradeRepository.Delete(grade);
                _unitOfWork.Save();

                return new DeleteGradeResponse()
                {
                    IsSuccess = true,
                };
            }
            catch
            {
                return new DeleteGradeResponse()
                {
                    IsSuccess = false,
                };
            }
        }
    }
}
