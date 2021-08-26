using AutoMapper;
using ClassAndStudent.Areas.Admin.Models;
using ClassAndStudent.School.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAndStudent.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateBatchModel, Batch>().ReverseMap();
            CreateMap<EditBatchModel, Batch>().ReverseMap();

        }
    }
}
