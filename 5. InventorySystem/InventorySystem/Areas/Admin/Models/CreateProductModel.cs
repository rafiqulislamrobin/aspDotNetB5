using InventorySystem.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using InventorySystem.Store.Business_Object;

namespace InventorySystem.Areas.Admin.Models
{
    public class CreateProductModel
    {

        public string Name { get; set; }
        public int Price { get; set; }

        private readonly IInvenStoryService _invenStoryService;
        public CreateProductModel()
        {
            _invenStoryService = Startup.AutofacContainer.Resolve<IInvenStoryService>();
        }
        public CreateProductModel(IInvenStoryService invenStoryService)
        {
            _invenStoryService = invenStoryService;
        }
        internal void CreateProduct()
        {
            var product = new Product()
            {
                Name = Name,
                Price = Price
            };
            _invenStoryService.CreateProduct(product);
        }
    }
}
