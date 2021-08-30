using Autofac;
using Microsoft.Extensions.Configuration;
using StockData.info.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.info
{
    public class StockModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
   

        public StockModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;


        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockDataDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssemblyName", _migrationAssemblyName)
               .SingleInstance();

            builder.RegisterType<StockDataDbContext>().As<IStockDataDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            //builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
            //   .InstancePerLifetimeScope();
            //builder.RegisterType<TicketRepository>().As<ITicketRepository>()
            //   .InstancePerLifetimeScope();
            //builder.RegisterType<BookingUnitOfWork>().As<IBookingUnitOfWork>()
            //   .InstancePerLifetimeScope();

            //builder.RegisterType<BookingService>().As<IBookingService>()
            //    .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
