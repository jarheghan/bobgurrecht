using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RepositoryPattern.Model.Catalog
{
    public class Product : IAggregateRoot
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int? Price { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescrption { get; set; }
        public string MetaTitle { get; set; }
        public string SKU { get; set; }
        public string ManufacturePartNo { get; set; }
        public int? StockQuantity { get; set; }
        public bool? DisplayStockAvaliable { get; set; }
        public bool? DisplayStockQuantity { get; set; }
        public bool? CallForPrice { get; set; }
        public int? OldPrice { get; set; }
        public float? Weight { get; set; }
        public float? Length { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
        public Guid ProductGuid { get; set; }
        public int PictureID { get; set; }
        [Required]
        public int CategoryID { get; set; }
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

    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
