using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Data;
using InventorySystem.Store.Context;
using InventorySystem.Store.Entity;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Store.Repositories
{
    public class StockReopository : Repository<Stock , int> , IStockReopository
    {
        public StockReopository(IStoreDbContext context)
            : base((DbContext)context)
        { 

        }
    }
}
