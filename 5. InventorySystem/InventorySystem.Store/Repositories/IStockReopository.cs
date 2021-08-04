using InventorySystem.Data;
using InventorySystem.Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store.Repositories
{
    public interface IStockReopository : IRepository<Stock, int>
    {
    }
}
