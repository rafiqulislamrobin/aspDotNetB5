using InventorySystem.Store.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store.Services
{
    public interface IInvenStoryService
    {
        void StockChecking(Product product, Stock stock);
        IList<Product> GetAllProducts();
        void CreateProduct(Product product);
    }
}
