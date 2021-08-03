using InventorySystem.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using InventorySystem.Store.Business_Object;

namespace InventorySystem.Areas.Admin.Models
{
    public class InventoryModel
    {
        public IList<Product> Products { get; set; }

        private IInvenStoryService _invenStoryService;
        public InventoryModel()
        {
            _invenStoryService = Startup.AutofacContainer.Resolve<IInvenStoryService>();
        }
        public InventoryModel(IInvenStoryService invenStoryService)
        {
            _invenStoryService = invenStoryService;
        }
      
        internal void LoadModelData()
        {
            Products = _invenStoryService.GetAllProducts();

        }
    }
}
