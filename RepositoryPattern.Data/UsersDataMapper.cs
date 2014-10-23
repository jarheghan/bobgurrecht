using RepositoryPattern.Model.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Data
{
    public class UsersDataMapper : AbstractDataMapper<UserMain>, IUserRepository
    {

        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override UserMain Map(dynamic result)
        {
            throw new NotImplementedException();
        }

        public UserMain GetSingleUser(string username)
        {
            throw new NotImplementedException();
        }

        public UserMain GetSingleUser(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserMain> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public void Add(UserMain item)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserMain item)
        {
            throw new NotImplementedException();
        }

        public void Update(UserMain item)
        {
            throw new NotImplementedException();
        }
    }
}
