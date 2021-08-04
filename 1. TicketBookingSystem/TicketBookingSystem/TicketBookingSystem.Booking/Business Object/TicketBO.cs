using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Booking.Business_Object
{
    public class TicketBO
    {
        public int Id { get; set; }

        public string destination { get; set; }
        public int fees { get; set; }
        public int customerId { get; set; }
        
    }
}
