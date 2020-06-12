using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Reflection;

namespace Server.Framework
{
    class NHibernateHelper
    {
        private static ISessionFactory mSessionFactory;
        public static string DatabaseFile { get; set; }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (mSessionFactory == null)
                    InitializeSessionFactory();
                return mSessionFactory;
            }
        }


        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }


        private static void InitializeSessionFactory()
        {
            mSessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .BuildSessionFactory();
        }
    }
}
