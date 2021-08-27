using Autofac;
using BookAndAuthor.Info.Context;
using BookAndAuthor.Info.Repositories;
using BookAndAuthor.Info.Services;
using BookAndAuthor.Info.Unit_Of_Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info
{

    public class LibraryModule : Module
    {


        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public LibraryModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryDbContext>()
              .AsSelf()
              .WithParameter("connectionString", _connectionString)
              .WithParameter("migrationAssemblyName", _migrationAssemblyName)
              .InstancePerLifetimeScope();

            builder.RegisterType<LibraryDbContext>().As<ILibraryDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<BookRepository>().As<IBookRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<LibraryUnitOfWork>().As<ILibraryUnitOfWork>()
               .InstancePerLifetimeScope();

            builder.RegisterType<BookService>().As<IBookService>()
             .InstancePerLifetimeScope();

            base.Load(builder);

        }
    }
}
