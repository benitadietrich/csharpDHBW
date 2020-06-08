using System.Collections.Generic;
using System.Windows;
using Aufgabe14.Interfaces.Infrastructure;
using NHibernate.Exceptions;

namespace Aufgabe14.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(string databaseFile)
        {
            NHibernateHelper.DatabaseFile = databaseFile;
        }

        public List<T> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var returnList = session.CreateCriteria<T>().List<T>();
                return returnList as List<T>;
            }
        }

        public bool Delete(T entity)
        {
            bool result;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entity);
                        transaction.Commit();
                        result = true;
                    }
                    catch (GenericADOException e)
                    {
                        MessageBox.Show($"Es wurde ein Film mit dem ausgewählten Genre gefunden.\n\nError DB: {e.Message}",
                            "Löschen wurde abgebrochen.", MessageBoxButton.OK, MessageBoxImage.Error);
                        result = false;
                    }
                }
            }
            return result;
        }
        public bool Save(T entity)
        {
            bool result;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                    result = true;
                }
            }
            return result;
        }
    }
}
