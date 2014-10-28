using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace RepositoryPattern.Data
{
    public class OrderItemsDataMapper : AbstractDataMapper<OrderItems>, IOrderItemsRepository
    {
        protected override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override OrderItems Map(dynamic result)
        {
            var orderItems = new OrderItems
            {
                ID = result.ort_id,
                OrderItemGuid = result.ort_guid,
                OrderID = result.ort_ord_id,
                ProductID = result.ort_prd_id,
                Quantity = result.ort_quantity,
                ProductVariationID = result.ort_prv_id,
                AddUser = result.ort_add_user,
                AddDate = result.ort_add_date,
                ChangeDate = result.ort_change_date,
                ChangeUser = result.ort_change_user,
                DeleteFlag = result.ort_delete_flag
            };
            return orderItems;
        }

        public void Add(OrderItems item)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderItems item)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItems item)
        {
            throw new NotImplementedException();
        }

        public int InsertOrderItems(OrderItems items)
        {
            var param = new
            {
                OrderItemGuid = Guid.NewGuid(),
                OrderID = items.OrderID,
                ProductID = items.ProductID,
                Quantity = items.Quantity,
                ProductVariationID = items.ProductVariationID,
                AddUser = items.AddUser,
                AddDate = items.AddDate,
                DeleteFlag = items.DeleteFlag

            };
            using (IDbConnection cn = Connection)
            {
                var i = cn.Query("", param);
            }
        }

        public IEnumerable<OrderItems> GetAllOrderItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItems> GetOrderItemsByOrderID(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
