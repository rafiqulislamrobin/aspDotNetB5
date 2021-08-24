using Autofac;
using DoctorAndPatient.Chember.Context;
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
                base.Load(builder);
            }
    }

    
}
