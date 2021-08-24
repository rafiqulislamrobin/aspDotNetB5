using Autofac;
using BuyAndSell.Data.Info.Context;
using BuyAndSell.Data.Info.Repositories;
using BuyAndSell.Data.Info.Services;
using BuyAndSell.Data.Info.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info
{
    public class BuyAndSellModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public BuyAndSellModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShopDbContext>()
              .AsSelf()
              .WithParameter("connectionString", _connectionString)
              .WithParameter("migrationAssemblyName", _migrationAssemblyName)
              .InstancePerLifetimeScope();

            builder.RegisterType<ShopDbContext>().As<IShopDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ShopUnitOfWork>().As<IShopUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<InfoService>().As<IInfoService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
