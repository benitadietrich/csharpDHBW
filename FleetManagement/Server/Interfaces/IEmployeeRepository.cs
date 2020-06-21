using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        bool IsEmployeeReferred(BusinessUnit businessUnit);

        bool UpdateEmployee(Employee emp);

        Employee GetEmployee(int id);

        Employee GetEmployeeByNumber(int number);
        
    }
}
