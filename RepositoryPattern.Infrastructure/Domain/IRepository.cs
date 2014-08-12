using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Infrastructure.Domain
{
    public interface IRepository<T>  where T : class, IAggregateRoot
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        void Delete(int id);
    }
}
