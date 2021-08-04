using ECommerceSystem.ProductInfo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ECommerceSystem.ProductInfo.Business_Object;
using ECommerceSystem.Models;

namespace ECommerceSystem.Areas.Admin.Models
{
    public class ProductModel
    {
        public IList<Product> Products { get; set; }
        private readonly IProductService _iProductService;
        public ProductModel()
        {
            _iProductService = Startup.AutofacContainer.Resolve<IProductService>();
        }
        public ProductModel(IProductService iProductService)
        {
            _iProductService = iProductService;
        }
        internal void LoadModelData()
        {
            Products = _iProductService.GetAllProduct();
        }
        internal object GetAllProductData(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iProductService.GetProducts(
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

        internal void Delete(int id)
        {
            _iProductService.DeleteProduct(id);
        }
    }
}
