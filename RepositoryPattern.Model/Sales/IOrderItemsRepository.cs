using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Sales
{
    public interface IOrderItemsRepository : IRepository<OrderItems>
    {
        int InsertOrderItems(OrderItems items);
        IEnumerable<OrderItems> GetAllOrderItems();
        IEnumerable<OrderItems> GetOrderItemsByOrderID(int Id);
    }
}
