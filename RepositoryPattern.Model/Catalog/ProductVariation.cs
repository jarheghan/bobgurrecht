using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Catalog
{
    public class ProductVariation : IAggregateRoot
    {
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int ID{ get; set; }
        public string AddUser{ get; set; }
        public string ChangeUser{ get; set; }
        public DateTime? AddDate{ get; set; }
        public DateTime? ChangeDate{ get; set; }
        public bool? DeleteFlag{ get; set; }
    }
}
