using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Business_Object;

namespace TicketBookingSystem.Booking.Services
{
    public interface IBookingService
    {
        IList<CustomerBO> GetAllCustomer();
    }
}
