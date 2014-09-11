using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;


namespace RepositoryPattern.Model.Media
{
    public class Picture : IAggregateRoot
    {
        public int ID { get; set; }
       
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName="image")]
        public byte[] PictureBinary { get; set; }

        public string MimeType { get; set; }
        public string SeoFileName { get; set; }
        public bool IsNew { get; set; }
        public string FilePath { get; set; }



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

        public bool? DeleteFlag
        {
            get;
            set;
        }
    }
}
