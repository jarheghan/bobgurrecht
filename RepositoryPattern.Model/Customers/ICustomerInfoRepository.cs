using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Customers
{
    public interface ICustomerInfoRepository : IRepository<CustomerInfo>
    {
        Country MapCountry(dynamic items);
        State MapState(dynamic items);
        int InsertCustomerInfo(CustomerInfo items);
        IEnumerable<CustomerInfo> GetAllCustomersInfo();
        IEnumerable<Country> GetAllCountries();
        IEnumerable<State> GetAllStates();
        CustomerInfo GetSingleCustomerInfo(int cusId);
    }
}
