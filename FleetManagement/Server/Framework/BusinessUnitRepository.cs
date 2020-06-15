﻿using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Framework
{
    public class BusinessUnitRepository : Repository<BusinessUnit>
    {
        public BusinessUnitRepository(string databaseFile) : base(databaseFile)
        {

        }

        public BusinessUnit GetBusinessUnit(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                try
                {
                    return session.Query<BusinessUnit>()
                           .Where(x => x.Id == id).FirstOrDefault();

                }
                catch
                {
                    return null;
                }

            }
        }

        public bool UpdateBusinessUnit(BusinessUnit businessUnit)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var sessionUnit = session.Get<BusinessUnit>(businessUnit.Id);
                    session.Merge(businessUnit);
                    transaction.Commit();
                    return true;

                }
            }
        }
    }
}