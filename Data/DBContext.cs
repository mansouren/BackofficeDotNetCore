using Data.Interfaces;
using PetaPoco;
using PetaPoco.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DBContext : IB2SConfigDBContext, IB2SMainDBContext, IB2STxnDBContext,IB2SSecurityDBContext,IB2SCommonDBContext
    {
        private readonly string connectionString;
        private object _lock = new object();
        private IDatabase? db = null;

        public DBContext(string connectionString) => this.connectionString = connectionString;

        public IDatabase Database
        {
            get
            {
                if (db == null)
                {
                    lock (_lock)
                    {
                        if (db == null)
                        {
                            db = DatabaseConfiguration.Build().
                                UsingProvider<SqlServerDatabaseProvider>().
                                UsingConnectionString(connectionString).
                                UsingDefaultMapper<ConventionMapper>(m =>
                                {
                                    m.InflectTableName = (inflector, s) => inflector.Pluralise(inflector.Underscore(s));
                                }).
                                Create();
                        }
                    }
                }
                return db;
            }
        }
    }
}
