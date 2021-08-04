using Autofac;
using ECommerceSystem.ProductInfo.Business_Object;
using ECommerceSystem.ProductInfo.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceSystem.Areas.Admin.Models
{
    public class CreateProductModel
    {
        public int? Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        [Required, Range(0, 1000000)]
        public int? Price { get; set; }

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
                Price = Price.Value
            };
            _iProductService.CreateProduct(product);
        }
    }
}
