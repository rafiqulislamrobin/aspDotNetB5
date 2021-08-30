using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.info.Context;
using StockData.info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.info.UnitOfWokr
{
    public class StockDataUnitOfWork : UnitOfWork, IStockDataUnitOfWork
    {
        public ICompanyRepository Company { get; private set; }
        public ICompanyRepository Member { get; private set; }

        public StockDataUnitOfWork(IStockDataDbContext context,
             ICompanyRepository company,
             ICompanyRepository member)
              : base((DbContext)context)
        {
            Company = company;
            Member = member;
        }
    }
}
