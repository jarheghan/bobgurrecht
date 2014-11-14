using RepositoryPattern.Model.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using log4net;


namespace RepositoryPattern.Data
{
    public class OrderItemsDataMapper : AbstractDataMapper<OrderItems>, IOrderItemsRepository
    {
        ILog log = LogManager.GetLogger(typeof(OrderItemsDataMapper));
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
            
        }

        public int InsertOrderItems(OrderItems items)
        {
            var param = new
            {

                OrderItemGuid = items.OrderItemGuid,
                OrderID = items.OrderID,
                ProductID = items.ProductID,
                Quantity = items.Quantity,
                ProductVariationID = items.ProductVariationID,
                AddUser = items.AddUser,
                AddDate = items.AddDate,
                DeleteFlag = items.DeleteFlag


            };
  
            var sql = @"Insert into OrderItem(ort_guid, ort_ord_id,ort_prd_id,ort_quantity,ort_prv_id
                          ,ort_add_user, ort_add_date, ort_delete_flag)
                        Values(@OrderItemGuid,@OrderID,@ProductID,@Quantity,@ProductVariationID,@AddUser,
                        @AddDate, @DeleteFlag)
                        Select @@IDENTITY";
            using (IDbConnection cn = Connection)
            {
                try
                {
                    int i = cn.Query<dynamic>(sql, param).FirstOrDefault();
                    return i;
                }

                catch(Exception ex)
                {
                    log.Error("Error Message", ex);
                    return -2;
                }
            }
        }

        public IEnumerable<OrderItems> GetAllOrderItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItems> GetOrderItemsByOrderID(int Id)
        {
            List<OrderItems> orderItems = new List<OrderItems>();
            var sql = @"select * from orderItem where ort_ord_id = @Id";
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Query<dynamic>(sql, new {Id = Id });
                    foreach (var orditem in i)
                    {
                        orderItems.Add(Map(orditem));
                    }
                    return orderItems.AsEnumerable();
                }

                catch (Exception ex)
                {
                    log.Error("Error Message", ex);
                    return null;
                }
            }
        }


        public bool OrderItemWithSameProductIDAndVariationIDAndOrderID(OrderItems items)
        {
            var param = new
            {
                OrderID = items.OrderID,
                ProductID = items.ProductID,
                ProductVariationID = items.ProductVariationID,
            };
            var sql = @"  select * from OrderItem
                          where ort_ord_id = @OrderID
	                        and ort_prd_id = @ProductID
	                        and ort_prv_id = @ProductVariationID ";
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Query<dynamic>(sql, param).FirstOrDefault();
                    
                    if(i != null)
                        return true;
                }

                catch (Exception ex)
                {
                    log.Error("Error Message", ex);
                    return false;
                }
            }
            return false;
        }


        public OrderItems GetSingleOrderItems(int OrderItemID)
        {
            var sql = @"select * from orderItem where ort_id = @OrderItemID";
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Query<dynamic>(sql, new { OrderItemID = OrderItemID });
                   
                      var orderitem = Map(i);

                      return orderitem;
                }

                catch (Exception ex)
                {
                    log.Error("Error Message", ex);
                    return null;
                }
            }
        }


        public int DeleteOrderItems(int Id)
        {
            var sql = @"DECLARE @TmpTable TABLE(ID int)
                        delete from OrderItem OUTPUT DELETED.ort_id into @TmpTable 
                        where ort_id = @Id   Select ID from @TmpTable";
            using (IDbConnection cn = Connection)
            {
                try
                {
                    var i = cn.Query<int>(sql, new {Id =Id }).FirstOrDefault();
                    return i;
                }

                catch (Exception ex)
                {
                    log.Error("Error Message", ex);
                    return -2;
                }
            }
        }


        public int UpdateOrderItems(OrderItems items)
        {
            var param = new
            {
                ID = items.ID,
                Quantity = items.Quantity,
            };

            var sql = @"DECLARE @MyTableVar table(ID int)
                        UPDATE OrderItem
                        SET ort_quantity = @Quantity OUTPUT inserted.ort_id into @MyTableVar
                        WHERE ort_id = @ID
                        Select ID from  @MyTableVar";
            using (IDbConnection cn = Connection)
            {
                try
                {
                    int i = cn.Query<int>(sql, param).FirstOrDefault();
                    return i;
                }

                catch (Exception ex)
                {
                    log.Error("Error Message", ex);
                    return -2;
                }
            }
        }
    }
}
