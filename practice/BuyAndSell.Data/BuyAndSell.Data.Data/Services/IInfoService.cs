using BuyAndSell.Data.Info.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyAndSell.Data.Info.Services
{
    public interface IInfoService
    {
        void CreateCustomer(CustomerBO customer);
        (IList<CustomerBO> records, int total, int totalDisplay) GetCutomers(int pageIndex, int pageSize,
                                                                 string searchText, string sortText);
        CustomerBO GetCustomer(int id);
        void UpdateCustomer(CustomerBO customer);
        void DeleteCustomer(int id);
    }
}
