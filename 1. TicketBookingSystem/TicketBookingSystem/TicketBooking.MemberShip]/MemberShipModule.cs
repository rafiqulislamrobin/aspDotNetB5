﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.MemberShip_.Context;

namespace TicketBooking.MemberShip_
{
    public class MemberShipModule : Module
    {

        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public MemberShipModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            //builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
            //   .InstancePerLifetimeScope();
            //builder.RegisterType<TicketRepository>().As<ITicketRepository>()
            //   .InstancePerLifetimeScope();
            //builder.RegisterType<BookingUnitOfWork>().As<IBookingUnitOfWork>()
            //   .InstancePerLifetimeScope();

            //builder.RegisterType<BookingService>().As<IBookingService>()
            //    .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

