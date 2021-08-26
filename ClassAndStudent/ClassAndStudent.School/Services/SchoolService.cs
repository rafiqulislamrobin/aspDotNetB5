using AutoMapper;
using ClassAndStudent.School.Business_Object;
using ClassAndStudent.School.Exceptions;
using ClassAndStudent.School.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Services
{
 
    public class SchoolService : ISchoolService
    {
        private readonly IschoolUnitOfwork _iSchoolUnitOfWork;
        private readonly IMapper _mapper;

        public SchoolService(IschoolUnitOfwork iSchoolUnitOfWork, IMapper mapper)
        {
            _iSchoolUnitOfWork = iSchoolUnitOfWork;
            _mapper = mapper;

        }

        public void CreateBatch(Batch batch)
        {
            if (batch == null)
                throw new InvalidParameterException("Batch was not found");



            _iSchoolUnitOfWork.Batch.Add(
         _mapper.Map<Entities.Batch>(batch));

            _iSchoolUnitOfWork.Save();
        }

     

        public (IList<Batch> records, int total, int totalDisplay) GetBatches(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var batchData = _iSchoolUnitOfWork.Batch.GetDynamic(
            string.IsNullOrWhiteSpace(searchText) ? null : x => x.ClassNumber.ToString().Contains(searchText),
            sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from batch in batchData.data
                              select _mapper.Map<Batch>(batch)).ToList();


            return (resultData, batchData.total, batchData.totalDisplay);
        }

        public void DeleteBatch(int id)
        {
            _iSchoolUnitOfWork.Batch.Remove(id);
            _iSchoolUnitOfWork.Save();
        }

       
        private bool IsNameAlreadyUsed(int classNumber) =>
                    _iSchoolUnitOfWork.Batch.GetCount(n => n.ClassNumber == classNumber) > 0;

        public Batch Getbatches(int id)
        {
            var batch = _iSchoolUnitOfWork.Batch.GetById(id);
            if (batch == null)
            {
                return null;
            }

            return _mapper.Map<Batch>(batch);
        }

        public void Updatebatch(Batch batch)
        {

            if (batch == null)
            {
                throw new InvalidOperationException("Doctor is missing");
            }
            if (IsNameAlreadyUsed(batch.Id))
            {
                throw new DuplicateException("Doctor name is already used");
            }
            var doctorInfo = _iSchoolUnitOfWork.Batch.GetById(batch.Id);
            if (doctorInfo != null)
            {
                _mapper.Map(batch, doctorInfo);

                _iSchoolUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Doctor is not available");
            }
        }
    }
}
