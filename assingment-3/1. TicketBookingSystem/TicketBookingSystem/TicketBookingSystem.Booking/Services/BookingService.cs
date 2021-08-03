using SocialNetworl.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Business_Object;
using TicketBookingSystem.Booking.Context;
using TicketBookingSystem.Booking.Exceptions;
using TicketBookingSystem.Booking.Unit_of_Work;

namespace TicketBookingSystem.Booking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingUnitOfWork _bookingUnitOfWork;

        public BookingService(IDatetimeUtility datetimeUtility, IBookingUnitOfWork bookingUnitOfWork)
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
                    Name = entity.Name,
                    Age = entity.Age,
                    Address = entity.Address
                };
                customers.Add(customer);
            }
            return customers;
        }

        public void CreateCustomer(CustomerBO customer)
        {
            if (customer == null)
                throw new InvalidParameterException("Customer was not found");
            
            if (IsNameAlreadyUsed(customer.Name))
                throw new DuplicateException("Customer Name");

            _bookingUnitOfWork.Customers.Add(
                    new Entites.Customer
                    {

                        Name = customer.Name,
                        Age =customer.Age,
                        Address = customer.Address

                    }
                   );
                _bookingUnitOfWork.Save();
            


        }

        public void BookingTicket ( CustomerBO customer, TicketBO ticket)
        {
            var customerEntity = _bookingUnitOfWork.Customers.GetById(customer.Id);
            if (customerEntity==null)
            {
                throw new InvalidOperationException("Course was not found"); 
            }
            if (customerEntity.Tickets == null)
                customerEntity.Tickets = new List<Entites.Ticket>();

            customerEntity.Tickets.Add(new Entites.Ticket
             {
                 Fees =ticket.fees,
                 Destination = ticket.destination,
             
            });

              _bookingUnitOfWork.Save();
            
          
        }

        private bool IsNameAlreadyUsed (string name)=>
            _bookingUnitOfWork.Customers.GetCount(n => n.Name == name) > 0;



    


    }
}
