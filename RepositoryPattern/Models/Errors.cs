using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPattern.Models
{
    public class Errors
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public object View { get; set; }
    }
}