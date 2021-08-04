using InventorySystem.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using InventorySystem.Store.Business_Object;
using InventorySystem.Models;

namespace InventorySystem.Areas.Admin.Models
{
    public class ProductListModel
    {
        public IList<Product> Products { get; set; }

        private IInvenStoryService _invenStoryService;
        public ProductListModel()
        {
            _invenStoryService = Startup.AutofacContainer.Resolve<IInvenStoryService>();
        }
        public ProductListModel(IInvenStoryService invenStoryService)
        {
            _invenStoryService = invenStoryService;
        }
      
        internal void LoadModelData()
        {
            Products = _invenStoryService.GetAllProducts();

        }
        internal object GetProducts(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _invenStoryService.GetProducts(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name", "Price" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Price.ToString(),      
                                record.Id.ToString()
                        }
                        ).ToArray()
            };

        }
    }
}
