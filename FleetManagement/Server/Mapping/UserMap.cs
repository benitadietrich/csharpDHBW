using FluentNHibernate.Mapping;
using Server.Models;

namespace Server.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Firstname).Length(50).Not.Nullable();
            Map(x => x.Lastname).Length(50).Not.Nullable();
            Map(x => x.Username).Length(20).Not.Nullable();
            Map(x => x.Password).Length(60).Not.Nullable();
            Map(x => x.IsAdmin).Not.Nullable();
        }
    }
}
