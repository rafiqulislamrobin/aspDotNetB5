using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
   public class Ticket
   {
        public int Id { get; set; }
       
        public string destination { get; set; }
        public int fees { get; set; }
        public Customer customerId { get; set; }
    }
}
