using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Customers
{
    public class UserMain :IAggregateRoot
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public string DisplayName { get; set; }
        public string Country { get; set; }

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
