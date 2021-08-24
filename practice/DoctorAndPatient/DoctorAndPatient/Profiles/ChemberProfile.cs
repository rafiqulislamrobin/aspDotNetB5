using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BO = DoctorAndPatient.Chember.Business_Object;
using EO = DoctorAndPatient.Chember.Entities;
namespace DoctorAndPatient.Profiles
{
    public class ChemberProfile : Profile
    {
        public ChemberProfile()
        {
            CreateMap<EO.Doctor, BO.Doctor>().ReverseMap();
            CreateMap<EO.Patient, BO.Patient>().ReverseMap();
        }
    }
}
