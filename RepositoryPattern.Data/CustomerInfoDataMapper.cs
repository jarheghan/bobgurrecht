using RepositoryPattern.Model.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Data
{
    public class CustomerInfoDataMapper : AbstractDataMapper<CustomerInfo>, ICustomerInfoRepository
    {
        public void Add(CustomerInfo item)
        {
            throw new NotImplementedException();
        }

        public void Remove(CustomerInfo item)
        {
            throw new NotImplementedException();
        }

        public void Update(CustomerInfo item)
        {
            throw new NotImplementedException();
        }

        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override CustomerInfo Map(dynamic result)
        {
            var customerInfo = new CustomerInfo
            {
                ID = result.cus_id,
                CustomerGuid = result.cus_guid,
                Email = result.cus_email,
                FirstName = result.cus_first_name,
                LastName = result.cus_last_name,
                Address1 = result.cus_address1,
                Address2 = result.cus_address2,
                Address3 = result.cus_address3,
                City = result.cus_city,
                State = result.cus_state,
                Country = result.cus_country,
                ZipCode = result.cus_zip,
                Company = result.cus_company,
                Phone = result.cus_phone,
                Active = result.cus_active,
                AdminComment = result.cus_admin_comment,
                AddUser = result.cus_add_user,
                AddDate = result.cus_add_date,
                ChangeDate = result.cus_change_date,
                ChangeUser = result.cus_change_user,
                DeleteFlag = result.cus_delete_flag

            };
            return customerInfo;
        }
    }
}
