using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Sales
{
    public class Order : IAggregateRoot
    {
        public string OrderNumber { get; set; }
        public Guid OrderGuid { get; set; }
        public bool Active { get; set; }
        public int UserID { get; set; }
        public int ID { get; set; }
        public string AddUser { get; set; }
        public string ChangeUser { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public bool? DeleteFlag { get; set; }
    }
}
