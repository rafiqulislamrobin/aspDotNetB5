using InventorySystem.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using InventorySystem.Store.Business_Object;

namespace InventorySystem.Areas.Admin.Models
{
    public class ProductCheckModel
    {
        public int stockId { get; set; }
        public string productName { get; set; }

        private readonly IInvenStoryService _invenStoryService;
        public ProductCheckModel()
        {
            _invenStoryService = Startup.AutofacContainer.Resolve<IInvenStoryService>();
        }
        public ProductCheckModel(IInvenStoryService invenStoryService)
        {
            _invenStoryService = invenStoryService;
        }
        public void StockChecking()
        {
            var products = _invenStoryService.GetAllProducts();
            var selectecdProduct = products.Where(x => x.Name == productName).FirstOrDefault();

            var stock= new Stock()
            {
                ProductId = 1,
                Qunatity = 20
         
            };
            _invenStoryService.StockChecking(selectecdProduct, stock);
        }
    }
}
