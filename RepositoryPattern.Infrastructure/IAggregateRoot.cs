using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Infrastructure
{
    public interface IAggregateRoot
    {
        int ID { get; set; }
        string AddUser { get; set; }
        string ChangeUser { get; set; }
        DateTime AddDate { get; set; }
        DateTime ChangeDate { get; set; }
        bool DeleteFlag { get; set; }
    }
}
