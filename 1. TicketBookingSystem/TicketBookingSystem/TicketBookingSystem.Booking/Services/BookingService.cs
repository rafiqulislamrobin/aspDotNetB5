using SocialNetworl.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Business_Object;
using TicketBookingSystem.Booking.Context;
using TicketBookingSystem.Booking.Entites;
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
                throw new InvalidOperationException("Customer was not found"); 
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


        public (IList<CustomerBO> records, int total, int totalDisplay) GetCutomers(int pageIndex, int pageSize,
        string searchText, string sortText)
        {
            var customerData = _bookingUnitOfWork.Customers.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from customer in customerData.data
                              select new CustomerBO
                              {
                                  Id = customer.Id ,
                                  Name = customer.Name,
                                  Age = customer.Age,
                                  Address = customer.Address
                              }).ToList();

            return (resultData, customerData.total, customerData.totalDisplay);
        }

        private bool IsNameAlreadyUsed(string name) =>
            _bookingUnitOfWork.Customers.GetCount(n => n.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name ,int id) =>
           _bookingUnitOfWork.Customers.GetCount(n => n.Name == name && n.Id!=id) > 0;

        public CustomerBO GetCustomer(int id)
        {

           var customer =  _bookingUnitOfWork.Customers.GetById(id);
            if (customer==null)
            {
                return null;
            }

            return new CustomerBO
            {
                Id = customer.Id,
                Name = customer.Name,
                Age = customer.Age,
                Address = customer.Address


            };
        }

        public void UpdateCustomer(CustomerBO customer)
        {
            if (customer==null)
            {
                throw new InvalidOperationException("Customer is missing");
            }
            if (IsNameAlreadyUsed(customer.Name, customer.Id))
            {
                throw new DuplicateException("Customer name is already used");
            }
            var customerInfo =_bookingUnitOfWork.Customers.GetById(customer.Id);
            if (customerInfo != null)
            {
                customerInfo.Id = customer.Id;
                customerInfo.Name = customer.Name;
                customerInfo.Age = customer.Age;
                customerInfo.Address = customer.Address;
                _bookingUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Customer is not available");
            }
            
        }

        public void DeleteCustomer(int id)
        {
            _bookingUnitOfWork.Customers.Remove(id);
            _bookingUnitOfWork.Save();
           
        }
    }
}
