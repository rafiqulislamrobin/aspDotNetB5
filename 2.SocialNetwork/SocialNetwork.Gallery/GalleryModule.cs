using Autofac;
using SocialNetwork.Gallery.Repository;
using SocialNetwork.Gallery.Services;
using SocialNetwork.Gallery.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Context
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
            builder.RegisterType<GalleryContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<GalleryContext>().As<IGalleryContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>()
          .InstancePerLifetimeScope();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>()
          .InstancePerLifetimeScope();
            builder.RegisterType<GalleryUnitOfWork>().As<IGalleryUnitOfWork>()
          .InstancePerLifetimeScope();

            builder.RegisterType<GalleryServices>().As<IGalleryServices>()
                .InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}