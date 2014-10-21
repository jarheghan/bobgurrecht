using RepositoryPattern.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Model.Sales
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
