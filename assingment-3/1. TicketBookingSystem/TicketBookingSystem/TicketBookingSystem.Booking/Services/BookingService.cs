using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Business_Object;
using TicketBookingSystem.Booking.Context;

namespace TicketBookingSystem.Booking.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingDbContext _bookingDbContext;
        public BookingService(BookingDbContext bookingDbContext )
        {
            _bookingDbContext = bookingDbContext;
        } 
        public IList<CustomerBO> GetAllCustomer()
        {

            var customerEntities = _bookingDbContext.customers.ToList();
            var customers = new List<CustomerBO>();

            foreach (var entity in customerEntities)
            {
                var customer = new CustomerBO()
                {
                    name = entity.name,
                    age = entity.age,
                    address = entity.address
                };
                customers.Add(customer);
            }
            return customers;
        }

       
    }
}
