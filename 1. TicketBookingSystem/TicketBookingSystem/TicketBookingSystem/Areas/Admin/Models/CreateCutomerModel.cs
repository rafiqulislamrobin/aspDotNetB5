using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Booking.Business_Object;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class CreateCutomerModel
    {
      
        public string Name{ get; set; }
        public int Age { get; set; }
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
