using FluentNHibernate.Mapping;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mapping
{
    public class VehicleMap : ClassMap<Vehicle>
    {
        public VehicleMap()
        {
            Table("Vehicles");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.LicensePlate).Length(50).Not.Nullable();
            Map(x => x.Brand).Length(50).Not.Nullable();
            Map(x => x.Model).Length(50).Not.Nullable();
            Map(x => x.Insurance).Precision(2).Not.Nullable();
            Map(x => x.LeasingFrom).Not.Nullable();
            Map(x => x.LeasingTo).Not.Nullable();
            Map(x => x.LeasingRate).Precision(2);

        }
    }
}
