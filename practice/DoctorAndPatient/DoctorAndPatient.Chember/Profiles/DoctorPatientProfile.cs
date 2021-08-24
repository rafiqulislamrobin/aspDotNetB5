using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = DoctorAndPatient.Chember.Business_Object;
using EO = DoctorAndPatient.Chember.Entities;

namespace DoctorAndPatient.Chember.Profiles
{
  
    public class DoctorPatientProfile : Profile
    {
        public DoctorPatientProfile()
        {
            CreateMap<EO.Doctor, BO.Doctor>().ReverseMap();
            
        }
    }
}
