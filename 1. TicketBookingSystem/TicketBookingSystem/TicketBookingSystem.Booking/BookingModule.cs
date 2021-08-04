using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Context;
using TicketBookingSystem.Booking.Repositories;
using TicketBookingSystem.Booking.Services;
using TicketBookingSystem.Booking.Unit_of_Work;

namespace TicketBookingSystem.Booking
{
    public class BookingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public BookingModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookingDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingDbContext>().As<IBookingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
               .InstancePerLifetimeScope();
            builder.RegisterType<TicketRepository>().As<ITicketRepository>()
               .InstancePerLifetimeScope();
            builder.RegisterType<BookingUnitOfWork>().As<IBookingUnitOfWork>()
               .InstancePerLifetimeScope();

            builder.RegisterType<BookingService>().As<IBookingService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

