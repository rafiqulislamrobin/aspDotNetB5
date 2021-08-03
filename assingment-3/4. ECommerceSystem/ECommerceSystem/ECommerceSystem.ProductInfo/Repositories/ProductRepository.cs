using ECommerceSystem.Data;
using ECommerceSystem.ProductInfo.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductInfo.Repositories
{
    public class ProductRepository : Repository<Product , int> , IProductRepository
    {
        public ProductRepository(IProductInfoDbContext context)
            :base((DbContext)context)
        {

        }
    }
}
