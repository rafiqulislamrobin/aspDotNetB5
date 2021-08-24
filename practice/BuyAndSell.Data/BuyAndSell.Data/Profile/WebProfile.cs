using AutoMapper;
using BuyAndSell.Data.Areas.admin.Models;
using BuyAndSell.Data.Info.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateCutomerModel, CustomerBO>().ReverseMap();
            CreateMap<EditCustomerModel, CustomerBO>().ReverseMap();

        }
    }
}
