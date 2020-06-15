using FluentNHibernate.Mapping;
using Server.Models;

namespace Server.Mapping
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.FirstName).Length(50).Not.Nullable();
            Map(x => x.LastName).Length(50).Not.Nullable();
            Map(x => x.EmployeeNumber).Not.Nullable().Unique();
            Map(x => x.Salutation).Length(50);
            Map(x => x.Title).Length(50);
            References(x => x.BusinessUnitId).Column("BusinessUnitId").Not.Nullable();
        }
    }
}
