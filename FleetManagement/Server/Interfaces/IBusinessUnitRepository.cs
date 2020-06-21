using Server.Models;

namespace Server.Interfaces
{
    public interface IBusinessUnitRepository: IRepository<BusinessUnit>
    {
        BusinessUnit GetBusinessUnit(int id);

        bool UpdateBusinessUnit(BusinessUnit businessUnit);

        BusinessUnit GetBusinessUnitName(string name);
    }
}
