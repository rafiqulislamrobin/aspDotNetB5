using InventorySystem.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using InventorySystem.Store.Business_Object;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Areas.Admin.Models
{
    public class CreateProductModel
    {

        public int? Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        [Required, Range(0, 1000000)]
        public int? Price { get; set; }

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
                Price = Price.Value
            };
            _invenStoryService.CreateProduct(product);
        }
    }
}
