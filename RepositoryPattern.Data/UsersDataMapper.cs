using RepositoryPattern.Model.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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

            var users = new UserMain
            {
                ID = result.Id,
                Username = result.Username,
                DisplayName = result.DisplayName,
                Country = result.Country
            };
            return users;
        }

        public UserMain GetSingleUser(string username)
        {
            UserMain usermain = new UserMain();
            using (IDbConnection cn = Connection)
            {
                var users = cn.Query<dynamic>("select * from Users where Username = @username", new { username = username }).FirstOrDefault();
                usermain = Map(users);
                return usermain;
            }
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
