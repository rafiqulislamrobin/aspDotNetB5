using BuyAndSell.Data.Dat;
using BuyAndSell.Data.Info.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Repositories
{
    public interface ICustomerRepository : IRepository <Customer,int>
    {
    }
}
