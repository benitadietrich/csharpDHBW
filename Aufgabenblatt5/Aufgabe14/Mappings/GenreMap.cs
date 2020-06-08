using Aufgabe14.Models;
using FluentNHibernate.Mapping;

namespace Aufgabe14.Mappings
{
    class GenreMap : ClassMap<Genre>
    {
        public GenreMap()
        {
            Table("Genres");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name).Not.Nullable();
        }
    }
}
