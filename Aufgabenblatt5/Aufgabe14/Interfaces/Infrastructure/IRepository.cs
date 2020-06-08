using System.Collections.Generic;

namespace Aufgabe14.Interfaces.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        bool Delete(T entity);
        bool Save(T entity);
    }
}
