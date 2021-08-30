using AutoMapper;
using StockData.info.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.info.Services
{
    public class StockService : IStockService
    {
        private readonly IStockDataDbContext _iStockDataDbContext;
        private readonly IMapper _mapper;

        public StockService(IStockDataDbContext iStockDataDbContext, IMapper mapper)
        {
            _iStockDataDbContext = iStockDataDbContext;
            _mapper = mapper;

        }
    }
}
