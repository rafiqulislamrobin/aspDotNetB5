using Autofac;
using InventorySystem.Store.Context;
using InventorySystem.Store.Repositories;
using InventorySystem.Store.Services;
using InventorySystem.Store.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Store
{
    public class InventoryModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InventoryModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StoreDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StoreDbContext>()
             .As<IStoreDbContext>()
             .WithParameter("connectionString", _connectionString)
             .WithParameter("migrationAssemblyName", _migrationAssemblyName)
             .InstancePerLifetimeScope();


            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockReopository>().As<IStockReopository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<InventoryService>().As<IInvenStoryService>()
                .InstancePerLifetimeScope(); 

            builder.RegisterType<InventoryUnitOfWork>().As<IInventoryUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
