using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Customers
{
    public class Country
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public string ISO3 { get; set; }
        public string IDDCode { get; set; }
        public int MyProperty { get; set; }
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
