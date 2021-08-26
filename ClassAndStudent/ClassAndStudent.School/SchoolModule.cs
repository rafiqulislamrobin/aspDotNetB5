using Autofac;
using ClassAndStudent.School.Context;
using ClassAndStudent.School.Repositories;
using ClassAndStudent.School.Services;
using ClassAndStudent.School.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School
{

    public class SchoolModule : Module
    {


        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public SchoolModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SchoolDbContext>()
              .AsSelf()
              .WithParameter("connectionString", _connectionString)
              .WithParameter("migrationAssemblyName", _migrationAssemblyName)
              .InstancePerLifetimeScope();

            builder.RegisterType<SchoolDbContext>().As<ISchoolDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<BatchRepository>().As<IBatchRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<schoolUnitOfwork>().As<IschoolUnitOfwork>()
               .InstancePerLifetimeScope();

            builder.RegisterType<SchoolService>().As<ISchoolService>()
             .InstancePerLifetimeScope();

            base.Load(builder);

        }
    }
}
