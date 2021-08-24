using AutoMapper;
using DoctorAndPatient.Chember.Business_Object;
using DoctorAndPatient.Chember.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Services
{
    public class ChemberService : IChemberService
    {
        private readonly IChemberUnitOfWork _iChemberUnitOfWork;
        private readonly IMapper _mapper;

        public ChemberService(IChemberUnitOfWork iChemberUnitOfWork , IMapper mapper)
        {
            _iChemberUnitOfWork = iChemberUnitOfWork;
            _mapper = mapper;

        }

        public (IList<Doctor> records, int total, int totalDisplay) GetDoctors(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var doctorData = _iChemberUnitOfWork.Doctor.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from doctor in doctorData.data
                              select new Doctor
                              {
                                  Id = doctor.Id,
                                  Name = doctor.Name,
                                  Department = doctor.Department,
                                  Degree = doctor.Degree
                              }).ToList();

            return (resultData, doctorData.total, doctorData.totalDisplay);
        }
        private bool IsNameAlreadyUsed(string name) =>
              _iChemberUnitOfWork.Doctor.GetCount(n => n.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
              _iChemberUnitOfWork.Doctor.GetCount(n => n.Name == name && n.Id != id) > 0;
    }
}
