using FluentNHibernate.Mapping;
using Server.Models;

namespace Server.Mapping
{
    class BusinessUnitMap : ClassMap<BusinessUnit>
    {
        public BusinessUnitMap()
        {
            Table("BusinessUnits");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name).Length(100).Not.Nullable();
            Map(x => x.Description).Length(250);
            Version(x => x.Version).Not.Nullable().Default(1);
        }
    }
}
