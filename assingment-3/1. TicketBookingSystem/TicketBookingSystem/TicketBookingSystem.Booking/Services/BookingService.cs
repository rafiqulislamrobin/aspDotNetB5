using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Business_Object;
using TicketBookingSystem.Booking.Context;
using TicketBookingSystem.Booking.Unit_of_Work;

namespace TicketBookingSystem.Booking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;
        public BookingService(IBookingUnitOfWork bookingUnitOfWork)
        {
            _bookingUnitOfWork = bookingUnitOfWork;
        } 
        public IList<CustomerBO> GetAllCustomer()
        {

            var customerEntities = _bookingUnitOfWork.Customers.GetAll();
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

        public void CreateCustomer(CustomerBO customer)
        {
            _bookingUnitOfWork.Customers.Add(
                new Entites.Customer
                {
                    Id =customer.Id,
                    name=customer.name,
                    address=customer.address
                });
            _bookingUnitOfWork.Save();
        }

       
    }
}
