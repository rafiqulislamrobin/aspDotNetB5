using AutoMapper;
using DoctorAndPatient.Chember.Business_Object;
using DoctorAndPatient.Chember.Exceptions;
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

        public void CreateDoctor(Doctor doctor)
        {
            if (doctor == null)
                throw new InvalidParameterException("Customer was not found");

            if (IsNameAlreadyUsed(doctor.Name))
                throw new DuplicateException("Customer Name");

            _iChemberUnitOfWork.Doctor.Add(
         _mapper.Map<Entities.Doctor>(doctor)
        );
            _iChemberUnitOfWork.Save();
        }

        public void DeleteCustomer(int id)
        {
            _iChemberUnitOfWork.Doctor.Remove(id);
            _iChemberUnitOfWork.Save();

        }

        public (IList<Doctor> records, int total, int totalDisplay) GetDoctors(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var doctorData = _iChemberUnitOfWork.Doctor.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from doctor in doctorData.data
                              select _mapper.Map<Doctor>(doctor)).ToList();
                       

            return (resultData, doctorData.total, doctorData.totalDisplay);
        }

        public Doctor GetDoctors(int id)
        {
            var doctor = _iChemberUnitOfWork.Doctor.GetById(id);
            if (doctor == null)
            {
                return null;
            }

            return _mapper.Map<Doctor>(doctor);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new InvalidOperationException("Doctor is missing");
            }
            if (IsNameAlreadyUsed(doctor.Name, doctor.Id))
            {
                throw new DuplicateException("Doctor name is already used");
            }
            var doctorInfo = _iChemberUnitOfWork.Doctor.GetById(doctor.Id);
            if (doctorInfo != null)
            {
                _mapper.Map(doctor, doctorInfo);

                _iChemberUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Doctor is not available");
            }
        }

        private bool IsNameAlreadyUsed(string name) =>
              _iChemberUnitOfWork.Doctor.GetCount(n => n.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
              _iChemberUnitOfWork.Doctor.GetCount(n => n.Name == name && n.Id != id) > 0;
    }
}
