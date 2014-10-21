using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Sales
{
    public class OrderItems : IAggregateRoot
    {
        public Guid OrderItemGuid { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int ProductVariationID { get; set; }
        public int ID {get; set;}
        public string AddUser {get; set;}
        public string ChangeUser {get; set;}
        public DateTime? AddDate {get; set;}
        public DateTime? ChangeDate {get; set;}
        public bool? DeleteFlag {get; set;}
    }
}
