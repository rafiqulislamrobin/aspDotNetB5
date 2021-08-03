using ECommerceSystem.ProductInfo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ECommerceSystem.ProductInfo.Business_Object;

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
    }
}
