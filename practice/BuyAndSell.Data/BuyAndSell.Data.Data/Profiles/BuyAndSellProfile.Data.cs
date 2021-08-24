using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = BuyAndSell.Data.Info.Business_Object;
using EO = BuyAndSell.Data.Info.Entities;

namespace BuyAndSell.Data.Info.Profiles
{
    public class BuyAndSellProfile : Profile
    {
        public BuyAndSellProfile()
        {
            CreateMap<EO.Customer, BO.CustomerBO>().ReverseMap();
        }
    }
}

