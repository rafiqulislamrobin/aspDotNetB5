using Autofac;
using InventorySystem.Store.Business_Object;
using InventorySystem.Store.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Models
{
    public class EditProductModel

    {


        public int? Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        [Required, Range(1, 1000000)]
        public int? Price { get; set; }


        private readonly IInvenStoryService _invenStoryService;
        public EditProductModel()
        {
            _invenStoryService = Startup.AutofacContainer.Resolve<IInvenStoryService>();
        }
        public EditProductModel(IInvenStoryService invenStoryService)
        {
            _invenStoryService = invenStoryService;
        }

        public void LoadModelData(int id)
        {
            var product = _invenStoryService.GetProduct(id);

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
            _invenStoryService.UpdateProduct(product);
        }
    }
}
