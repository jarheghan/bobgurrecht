using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Customers
{
    public class State : IAggregateRoot
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }
        public String AbbreviatedName { get; set; }
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
