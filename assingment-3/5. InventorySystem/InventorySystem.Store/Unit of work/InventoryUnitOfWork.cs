using InventorySystem.Data;
using InventorySystem.Store.Context;
using InventorySystem.Store.Entity;
using InventorySystem.Store.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store.Unit_of_work
{
    public class InventoryUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public IStockReopository Stocks { get; private set; }
        public IProductRepository Products { get; private set; }

        public InventoryUnitOfWork(IStoreDbContext context,
            IProductRepository products,
            IStockReopository stocks)
            :base ((DbContext)context)
        {
            Stocks = stocks;
            Products = products;
        }

    }
}
