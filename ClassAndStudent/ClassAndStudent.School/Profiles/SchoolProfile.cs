using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = ClassAndStudent.School.Business_Object;
using EO = ClassAndStudent.School.Entities;

namespace ClassAndStudent.School.Profiles
{
    class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<EO.Batch, BO.Batch>().ReverseMap();

        }
    }
}
