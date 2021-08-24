using BuyAndSell.Data.Dat;
using BuyAndSell.Data.Info.Context;
using BuyAndSell.Data.Info.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(IShopDbContext context)
    : base((DbContext)context)
        {

        }
    }
}
