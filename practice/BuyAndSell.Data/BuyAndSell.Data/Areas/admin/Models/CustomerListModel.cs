using Autofac;
using BuyAndSell.Data.Info.Services;
using BuyAndSell.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Areas.admin.Models
{
    public class CustomerListModel
    {

        private readonly IInfoService _iInfoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerListModel()
        {
            _iInfoService = Startup.AutofacContainer.Resolve<IInfoService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public CustomerListModel(IInfoService iInfoService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _iInfoService = iInfoService;
        }
        internal object GetCustomers(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iInfoService.GetCutomers(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name", "Age", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Age.ToString(),
                                record.Address.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }

        internal void Delete(int id)
        {
            _iInfoService.DeleteCustomer(id);
        }
    }
}
