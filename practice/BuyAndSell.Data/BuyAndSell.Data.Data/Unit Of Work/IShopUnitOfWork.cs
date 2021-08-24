using BuyAndSell.Data.Dat;
using BuyAndSell.Data.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Unit_Of_Work
{
    public interface IShopUnitOfWork : IUnitOfWork
    {
        ICustomerRepository Customer { get;}
        IProductRepository Product { get; }
    }
}
