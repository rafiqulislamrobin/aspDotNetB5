using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Context;
using TicketBookingSystem.Booking.Entites;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public class TicketRepository : Repository<Ticket , int> , ITicketRepository
    {
        public TicketRepository(IBookingDbContext context)
            :base ((DbContext)context)
        {

        }
    }
}
