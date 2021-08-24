using AutoMapper;
using BuyAndSell.Data.Info.Business_Object;
using BuyAndSell.Data.Info.Entities;
using BuyAndSell.Data.Info.Exceptions;
using BuyAndSell.Data.Info.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Services
{
    public class InfoService : IInfoService
    {
        private readonly IShopUnitOfWork _iShopUnitOfWork;
        private readonly IMapper _mapper;

        public InfoService(IShopUnitOfWork iShopUnitOfWork , IMapper mapper)
        {
            _iShopUnitOfWork = iShopUnitOfWork;
            _mapper = mapper;

        }
        public void CreateCustomer(CustomerBO customer)
        {
            if (customer == null)
                throw new InvalidParameterException("Customer was not found");

            if (IsNameAlreadyUsed(customer.Name))
                throw new DuplicateException("Customer Name already exist");

                    _iShopUnitOfWork.Customer.Add(
                 _mapper.Map<Entities.Customer>(customer)
                );
                    _iShopUnitOfWork.Save();

        }

        public void DeleteCustomer(int id)
        {
            _iShopUnitOfWork.Customer.Remove(id);
            _iShopUnitOfWork.Save();

        }

        public CustomerBO GetCustomer(int id)
        {
            var customer = _iShopUnitOfWork.Customer.GetById(id);
            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerBO>(customer);
 
        }

        public (IList<CustomerBO> records, int total, int totalDisplay) GetCutomers(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var customerData = _iShopUnitOfWork.Customer.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from customer in customerData.data
                              select _mapper.Map<CustomerBO>(customer)).ToList();

            return (resultData, customerData.total, customerData.totalDisplay);
        }

        public void UpdateCustomer(CustomerBO customer)
        {
            if (customer == null)
            {
                throw new InvalidOperationException("Customer is missing");
            }
            if (IsNameAlreadyUsed(customer.Name, customer.Id))
            {
                throw new DuplicateException("Customer name is already used");
            }
            var customerInfo = _iShopUnitOfWork.Customer.GetById(customer.Id);
            if (customerInfo != null)
            {
                _mapper.Map(customer, customerInfo);
             
                _iShopUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Customer is not available");
            }
        }

        private bool IsNameAlreadyUsed(string name) =>
            _iShopUnitOfWork.Customer.GetCount(n => n.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
            _iShopUnitOfWork.Customer.GetCount(n => n.Name == name && n.Id != id) > 0;

    }
}
