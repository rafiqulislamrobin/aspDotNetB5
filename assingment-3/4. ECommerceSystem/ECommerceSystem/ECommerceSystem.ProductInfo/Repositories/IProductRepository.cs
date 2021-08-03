using ECommerceSystem.Data;
using ECommerceSystem.ProductInfo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductInfo.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
