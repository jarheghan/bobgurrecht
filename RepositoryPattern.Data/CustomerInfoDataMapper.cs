using RepositoryPattern.Model.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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

        public Country MapCountry(dynamic items)
        {
            var country = new Country
            {
                ID = items.cnt_Id,
                Name = items.cnt_name,
                ISO3 = items.cnt_iso_3,
                IDDCode = items.cnt_idd_code,
                AddUser = items.cnt_add_user,
                AddDate = items.cnt_add_date,
                ChangeDate = items.cnt_change_date,
                ChangeUser = items.cnt_change_user,
                DeleteFlag = items.cnt_delete_flag

            };
            return country;
        }

        public State MapState(dynamic items)
        {
            var states = new State
            {
                ID = items.sta_Id,
                CountryID = items.sta_cnt_Id,
                Name = items.sta_name,
                AbbreviatedName = items.sta_abbr,
                AddUser = items.sta_add_user,
                AddDate = items.sta_add_date,
                ChangeDate = items.sta_change_date,
                ChangeUser = items.sta_change_user,
                DeleteFlag = items.sta_delete_flag
            };
            return states;
        }

        public int InsertCustomerInfo(CustomerInfo items)
        {
            var param = new CustomerInfo
            {
                ID = items.ID,
                CustomerGuid = items.CustomerGuid,
                FirstName = items.FirstName,
                LastName = items.LastName,
                Address1 = items.Address1,
                Address2 = items.Address2,
                Address3 = items.Address3,
                Active = items.Active,
                Company = items.Company,
                City = items.City,
                State = items.State,
                Country = items.Country,
                Email = items.Email,
                ZipCode = items.ZipCode,
                Phone = items.Phone,
                AdminComment = items.AdminComment,
                AddDate = items.AddDate,
                AddUser = items.AddUser,
                ChangeDate = items.ChangeDate,
                ChangeUser = items.ChangeUser,
                DeleteFlag = items.DeleteFlag

            };
            using (IDbConnection cn = Connection)
            {
                var sql = @"INSERT INTO CustomerInfo (cus_id, cus_guid, cus_email, cus_first_name, cus_last_name, cus_address1, cus_address2,
                                                        cus_city,cus_state,cus_country,cus_zip,cus_phone,cus_active,cus_add_user,cus_add_date,cus_delete_flag)
                                                       Values(@ID,@CustomerGuid,@Email,@FirstName,@LastName,@Address1,@Address2,@City,@State
                                                               ,@Country,@ZipCode,@Phone,@Active,@AddUser,@AddDate,@DeleteFlag)
                                                            select @@ROWCOUNT";
                            
                var i = cn.Query<int>(sql, param).FirstOrDefault();
                return (int)i;
            }
        }

        public IEnumerable<CustomerInfo> GetAllCustomersInfo()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Country> GetAllCountries()
        {
            List<Country> country = new List<Country>();
            using (IDbConnection cn = Connection)
            {
                var countries = cn.Query(@"select * from country");
                foreach (var cnt in countries)
                {
                    country.Add(MapCountry(cnt));
                }
                return country.AsEnumerable();
            }
        }

        public IEnumerable<State> GetAllStates()
        {
            List<State> state = new List<State>();
            using (IDbConnection cn = Connection)
            {
                var states = cn.Query(@"select * from states");
                foreach (var sta in states)
                {
                    state.Add(MapState(sta));
                }
                return state.AsEnumerable();
            }
        }
    }
}
