using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Customers
{
    public interface IUserRepository : IRepository<UserMain>
    {
        UserMain GetSingleUser(string username);
        UserMain GetSingleUser(int Id);
        IEnumerable<UserMain> GetAllUser();
    }
}
