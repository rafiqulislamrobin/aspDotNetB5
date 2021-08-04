﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Booking.Business_Object;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class CreateCutomerModel
    {

        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name{ get; set; }

        [Required, Range(0, 150)]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        private readonly IBookingService _bookingService;
        public CreateCutomerModel()
        {
            _bookingService = Startup.AutofacContainer.Resolve<IBookingService>();
        }
        public CreateCutomerModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        internal void CreateCustomer()
        {
            var customer = new CustomerBO()
            {
                Name = Name,
                Age = Age,
                Address = Address,
               
            };
            _bookingService.CreateCustomer(customer);
        }
    }
}
