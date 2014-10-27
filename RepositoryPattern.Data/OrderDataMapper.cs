using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
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

        public bool OrderUserExist(int UserId)
        {
            using (IDbConnection cn = Connection)
            {
                var i = cn.Query<dynamic>("select * from order where ord_usr_id = @UserId", new { UserId = UserId }).FirstOrDefault();
                if (i == null)
                    return false;
                else return true;

            }
            throw new NotImplementedException();
        }


        public int InsertOrders(Order items)
        {
            var param = new
            {
                OrderGuid = Guid.NewGuid(),
                Active = items.Active,
                UserID = items.UserID,
                AddUser = items.AddUser,
                AddDate = DateTime.Now,
                DeleteFlag = false

            };
            using (IDbConnection cn = Connection)
            {
                int i = cn.Query<dynamic>(@"DECLARE @TmpTable TABLE(ID int)
                                    Insert into order(ord_guid, ord_active, ord_usr_id,ord_add_date, ord_add_user, ord_delete_flag)
                                    OUTPUT Inserted.ord_id INTO @TmpTable
                                    Values(@OrderGuid,@Active,@UserID,@AddUser,@AddDate,@DeleteFlag)
                                    select ID from @TmpTable
                                    ", param).FirstOrDefault();

                return i;
            }
        }

        public IEnumerable<Order> GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public Order GetSingleOrderByOrderID(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByUserId(int Id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByUserID(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
