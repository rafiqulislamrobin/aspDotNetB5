using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Entites
{
   public class Ticket : IEntity<int>
    {
        public int Id { get; set; }
       
        public string Destination { get; set; }
        public int Fees { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
