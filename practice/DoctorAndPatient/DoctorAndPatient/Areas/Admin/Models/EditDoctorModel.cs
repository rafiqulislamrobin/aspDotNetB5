using Autofac;
using AutoMapper;
using DoctorAndPatient.Chember.Business_Object;
using DoctorAndPatient.Chember.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAndPatient.Areas.Admin.Models
{
    public class EditDoctorModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Departement { get; set; }
        public string Degree { get; set; }

        private readonly IChemberService _iChemberService;
        private readonly IMapper _mapper;

        public EditDoctorModel()
        {
            _iChemberService = Startup.AutofacContainer.Resolve<IChemberService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public EditDoctorModel(IChemberService iChemberService, IMapper mapper)
        {
            _iChemberService = iChemberService;
            _mapper = mapper;
        }


        internal void LoadModelData(int id)
        {
            var doctor = _iChemberService.GetDoctors(id);

            Id = doctor.Id;
            Name = doctor.Name;
            Departement = doctor.Department;
            Degree = doctor.Degree;
        }

        internal void Update()
        {
            var doctor = new Doctor
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                Department = Departement,
                Degree =Degree
                

            };
            _iChemberService.UpdateDoctor(doctor);
        }
    }
}
