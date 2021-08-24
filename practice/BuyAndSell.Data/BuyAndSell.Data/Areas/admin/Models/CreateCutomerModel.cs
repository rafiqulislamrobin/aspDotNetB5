using Autofac;
using AutoMapper;
using BuyAndSell.Data.Info.Business_Object;
using BuyAndSell.Data.Info.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Areas.admin.Models
{
    public class CreateCutomerModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Age { get; set; }

        private readonly IInfoService _iInfoService;
        private readonly IMapper _mapper;
        public CreateCutomerModel()
        {
            _iInfoService = Startup.AutofacContainer.Resolve<IInfoService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateCutomerModel(IInfoService iInfoService , IMapper mapper)
        {
            _mapper = mapper;
            _iInfoService = iInfoService;
        }
        internal void CreateCustomer()
        {
            var customer = _mapper.Map<CustomerBO>(this);

            _iInfoService.CreateCustomer(customer);
        }

    }
}
