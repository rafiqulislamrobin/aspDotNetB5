using AutoMapper;
using DoctorAndPatient.Areas.Admin.Models;
using DoctorAndPatient.Chember.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAndPatient.Profiles
{
    public class ChemberProfile : Profile
    {
        public ChemberProfile()
        {
            CreateMap<CreateDoctorModel, Doctor>().ReverseMap();
            CreateMap<EditDoctorModel, Doctor>().ReverseMap();

        }
    }
}
