using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Data
{
    public class DataModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public DataModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AttendanceDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
