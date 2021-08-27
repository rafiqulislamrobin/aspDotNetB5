using Autofac;
using Gallery.MemberAndImages.Context;
using Gallery.MemberAndImages.Repositories;
using Gallery.MemberAndImages.Services;
using Gallery.MemberAndImages.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages
{

    public class GalleryModule : Module
    {


        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public GalleryModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalleryDbContext>()
              .AsSelf()
              .WithParameter("connectionString", _connectionString)
              .WithParameter("migrationAssemblyName", _migrationAssemblyName)
              .InstancePerLifetimeScope();

            builder.RegisterType<GalleryDbContext>().As<IGalleryDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<MemberRepository>().As<IMemberRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<GalleryService>().As<IGalleryService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<GalleryUnitOfWork>().As<IGalleryUnitOfWork>()
             .InstancePerLifetimeScope();

            base.Load(builder);

        }
    }
}
