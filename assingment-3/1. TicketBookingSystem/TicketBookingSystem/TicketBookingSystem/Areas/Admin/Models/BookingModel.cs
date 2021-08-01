using TicketBookingSystem.Booking.Services;
using Autofac;
using TicketBookingSystem.Booking.Business_Object;
using System.Collections.Generic;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class BookingModel
    {
        public IList<CustomerBO> Customers { get; set; }

        private readonly IBookingService _bookingService;
        public BookingModel()
        {
            _bookingService = Startup.AutofacContainer.Resolve<IBookingService>();
        }
        public BookingModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public void LoadModelData()
        {
            Customers = _bookingService.GetAllCustomer();
        }
    }
}
