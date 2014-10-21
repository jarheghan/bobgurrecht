using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Data
{
    public class OrderDataMapper : AbstractDataMapper<Order>, IOrderRepository
    {
        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override Order Map(dynamic result)
        {
            var order = new Order
            {
                ID = result.ord_id,
                OrderGuid = result.ord_guid,
                OrderNumber = result.ord_number,
                Active = result.ord_active,
                UserID = result.ord_use_id,
                AddUser = result.ord_add_user,
                AddDate = result.ord_add_date,
                ChangeDate = result.ord_change_date,
                ChangeUser = result.ord_change_user,
                DeleteFlag = result.ord_delete_flag
            };
            return order;
        }

        public void Add(Order item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Order item)
        {
            throw new NotImplementedException();
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
