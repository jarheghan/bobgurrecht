using RepositoryPattern.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Data
{
    public class UserDataMapper : AbstractDataMapper<User>
    {
        protected override string TableName
        {
            get { return "User"; }
        }

        public override User Map(dynamic result)
        {
            var item = new User
            {
                UserID = result.ID,
                UserName = result.UserName
            };

            return item;
        }
    }
}
