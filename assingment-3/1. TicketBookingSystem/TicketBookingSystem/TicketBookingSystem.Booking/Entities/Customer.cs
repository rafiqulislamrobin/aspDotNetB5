﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Entites
{
    public class Customer : IEntity<int>
    {

        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
