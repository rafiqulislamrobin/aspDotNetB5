using BuyAndSell.Data.Dat;
using BuyAndSell.Data.Info.Context;
using BuyAndSell.Data.Info.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Unit_Of_Work
{
    public class ShopUnitOfWork : UnitOfWork, IShopUnitOfWork
    {
        public ICustomerRepository Customer { get; private set; }
        public IProductRepository Product { get; private set; }

        public ShopUnitOfWork(IShopDbContext context,
             ICustomerRepository customers,
             IProductRepository products)
              : base((DbContext)context)
        {
               Customer = customers;
               Product = products;
        }
    }
}
