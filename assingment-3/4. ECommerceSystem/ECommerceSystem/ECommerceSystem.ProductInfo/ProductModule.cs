using Autofac;
using ECommerceSystem.ProductInfo.Repositories;
using ECommerceSystem.ProductInfo.Service;
using ECommerceSystem.ProductInfo.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.ProductInfo
{
    public class ProductModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ProductModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductInfoDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            
                builder.RegisterType<ProductInfoDbContext>()
                    .As<IProductInfoDbContext>()
                    .WithParameter("connectionString", _connectionString)
                    .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                    .InstancePerLifetimeScope();


            builder.RegisterType<ProductRepository>().As<IProductRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<ProductUnitOfWork>().As<IProductUnitOfWork>()
              .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
