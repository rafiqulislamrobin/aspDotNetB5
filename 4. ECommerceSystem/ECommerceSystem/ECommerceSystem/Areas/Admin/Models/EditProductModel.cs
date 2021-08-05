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
    public class EditProductModel

    {


        public int? Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        [Required, Range(1, 1000000)]
        public int? Price { get; set; }


        private readonly IProductService _productService;
        public EditProductModel()
        {
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }
        public EditProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public void LoadModelData(int id)
        {
            var product = _productService.GetProduct(id);

            Id = product?.Id;
            Name = product?.Name;
            Price = product?.Price;

        }

        internal void Update()
        {
            var product = new Product
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                Price = Price.HasValue ? Price.Value : 0

            };
            _productService.UpdateProduct(product);
        }
    }
}
