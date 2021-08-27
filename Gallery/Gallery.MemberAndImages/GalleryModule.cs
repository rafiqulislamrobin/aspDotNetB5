using Autofac;
using Gallery.MemberAndImages.Context;
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

            //builder.RegisterType<DoctorRepository>().As<IDoctorRepository>()
            //  .InstancePerLifetimeScope();

            //builder.RegisterType<PatientRepository>().As<IPatientRepository>()
            //.InstancePerLifetimeScope();

            //builder.RegisterType<ChemberUnitOfWork>().As<IChemberUnitOfWork>()
            //   .InstancePerLifetimeScope();

            //builder.RegisterType<ChemberService>().As<IChemberService>()
            // .InstancePerLifetimeScope();

            base.Load(builder);

        }
    }
}
