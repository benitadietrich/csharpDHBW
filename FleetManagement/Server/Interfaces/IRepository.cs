using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IRepository<T> where T : class
    {

        List<T> GetAll();

        void Delete(T entity);

        void Save(T entity);
    }
}

