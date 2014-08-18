using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Catalog
{
    public class Category : IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int? ParentCategoryID { get; set; }
        public int? PictureID { get; set; }
        public int? PageSize { get; set; }
        public bool? AllowSelectedPageSize { get; set; }
        public string PriceRanges { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public int? DisplayOrder { get; set; }
        public int ID { get; set; }
        public string AddUser { get; set; }
        public string ChangeUser { get; set; }
        public DateTime? AddDate  { get; set; }
        public DateTime? ChangeDate  { get; set; }
        public bool? DeleteFlag  { get; set; }
        
    }
}
