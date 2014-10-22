using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Customers
{
    public class CustomerInfo :IAggregateRoot
    {
        public Guid CustomerGuid { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string AdminComment { get; set; }
        public bool Active { get; set; }
        public int ID
        {
            get;
            set;
        }

        public string AddUser
        {
            get;
            set;
        }

        public string ChangeUser
        {
            get;
            set;
        }

        public DateTime? AddDate
        {
            get;
            set;
        }

        public DateTime? ChangeDate
        {
            get;
            set;
        }
        public bool? DeleteFlag { get; set; }
    }
    }
}
