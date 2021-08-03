using InventorySystem.Data;
using InventorySystem.Store.Context;
using InventorySystem.Store.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store.Repositories
{
    public class ProductRepository : Repository<Product ,int> ,IProductRepository
    {
        public ProductRepository(IStoreDbContext context)
            :base ((DbContext)context)
        {

        }
    }
}
