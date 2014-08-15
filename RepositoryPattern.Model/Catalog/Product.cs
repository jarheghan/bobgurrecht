﻿using RepositoryPattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Catalog
{
    public class Product : IAggregateRoot
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int  Category { get; set; }
        public User CreatedBy { get; set; }

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

        public DateTime AddDate
        {
            get;
            set;
        }

        public DateTime ChangeDate
        {
            get;
            set;
        }
        public bool DeleteFlag { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}