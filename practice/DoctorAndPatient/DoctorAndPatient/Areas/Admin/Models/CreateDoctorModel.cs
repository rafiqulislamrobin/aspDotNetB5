using Autofac;
using AutoMapper;
using DoctorAndPatient.Chember.Business_Object;
using DoctorAndPatient.Chember.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAndPatient.Areas.Admin.Models
{
    public class CreateDoctorModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Degree { get; set; }

        private readonly IChemberService _iChemberService;
        private readonly IMapper _mapper;

        public CreateDoctorModel()
        {
            _iChemberService = Startup.AutofacContainer.Resolve<IChemberService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateDoctorModel(IChemberService iChemberService, IMapper mapper)
        {
            _mapper = mapper;
            _iChemberService = iChemberService;
        }
        internal void CreateDoctor()
        {
            
            var doctor = new Doctor()
            
            {
                Name = Name,
                Department = Department,
                Degree = Degree
            };
          

            _iChemberService.CreateDoctor(doctor);
        }
    }
}
