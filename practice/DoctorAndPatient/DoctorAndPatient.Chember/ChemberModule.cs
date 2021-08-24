using Autofac;
using DoctorAndPatient.Chember.Context;
using DoctorAndPatient.Chember.Repositories;
using DoctorAndPatient.Chember.Services;
using DoctorAndPatient.Chember.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember
{
    public class ChemberModule : Module
    {
        
        
            private readonly string _connectionString;
            private readonly string _migrationAssemblyName;

            public ChemberModule(string connectionString, string migrationAssemblyName)
            {
                _connectionString = connectionString;
                _migrationAssemblyName = migrationAssemblyName;

            }
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<ChemberContext>()
                  .AsSelf()
                  .WithParameter("connectionString", _connectionString)
                  .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                  .InstancePerLifetimeScope();

                builder.RegisterType<ChemberContext>().As<IChemberContext>()
                    .WithParameter("connectionString", _connectionString)
                    .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                    .InstancePerLifetimeScope();
                 
                builder.RegisterType<DoctorRepository>().As<IDoctorRepository>()
                  .InstancePerLifetimeScope();

                builder.RegisterType<PatientRepository>().As<IPatientRepository>()
                .InstancePerLifetimeScope();

                builder.RegisterType<ChemberUnitOfWork>().As<IChemberUnitOfWork>()
                   .InstancePerLifetimeScope();

               builder.RegisterType<ChemberService>().As<IChemberService>()
                .InstancePerLifetimeScope();

              base.Load(builder);

            }
    }

    
}
