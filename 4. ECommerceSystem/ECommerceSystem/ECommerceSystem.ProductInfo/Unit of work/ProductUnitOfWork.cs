using ECommerceSystem.Data;
using ECommerceSystem.ProductInfo.Entity;
using ECommerceSystem.ProductInfo.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductInfo.Unit_of_work
{
    public class ProductUnitOfWork : UnitOfWork, IProductUnitOfWork
    {
        public IProductRepository Products { get; set; }
       

        public ProductUnitOfWork(IProductInfoDbContext context,
            IProductRepository products)
            : base((DbContext)context)
        {
            Products = products;
        }

    }
}
