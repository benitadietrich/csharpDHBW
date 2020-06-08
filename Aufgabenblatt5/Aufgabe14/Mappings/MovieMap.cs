using Aufgabe14.Models;
using FluentNHibernate.Mapping;

namespace Aufgabe14.Mappings
{
    class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Table("Movies");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Titel).Column("Title").Length(100).Not.Nullable();
            References(x => x.Genre).Column("GenreID").Not.Nullable();
        }
    }
}
