using Autofac;
using ECommerceSystem.ProductInfo.Business_Object;
using ECommerceSystem.ProductInfo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceSystem.Areas.Admin.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public int Price { get; set; }

        private readonly IProductService _iProductService;
        public CreateProductModel()
        {
            _iProductService = Startup.AutofacContainer.Resolve<IProductService>();
        }
        public CreateProductModel(IProductService iProductService)
        {
            _iProductService = iProductService;
        }
        internal void CreateProduct()
        {
            var product = new Product()
            {
                Name = Name,
                Price = Price
            };
            _iProductService.CreateProduct(product);
        }
    }
}
