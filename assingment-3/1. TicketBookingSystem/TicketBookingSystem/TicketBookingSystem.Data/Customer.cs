using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
     public class Customer 
     {
        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string address { get; set; }
    }
}
