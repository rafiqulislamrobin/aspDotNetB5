using InventorySystem.Data;
using InventorySystem.Store.Entity;
using InventorySystem.Store.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store.Unit_of_work
{
    public interface IInventoryUnitOfWork : IUnitOfWork
    {
        public IStockReopository Stocks { get;  }
        public IProductRepository Products { get; }
    }
}
