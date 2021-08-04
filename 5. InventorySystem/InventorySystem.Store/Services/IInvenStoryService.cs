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
        (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, int pageSize,
                                                          string searchText, string sortText);
        Product GetProduct(int id);
        void UpdateProduct(Product student);
        void DeleteProduct(int id);
    }
}
