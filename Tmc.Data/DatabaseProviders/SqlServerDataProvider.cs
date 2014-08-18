using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Data;
using Tmc.Data.DatabaseContext;
using Tmc.Data.Initializers;

namespace Tmc.Data.DatabaseProviders
{
    public class SqlServerDataProvider : IDataProvider
    {
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new SqlConnectionFactory();
            #pragma warning disable 0618
            Database.DefaultConnectionFactory = connectionFactory;
        }

        public virtual void SetDatabaseInitializer()
        {
            //pass some table names to ensure that we tmc system installed
            var tablesToValidate = new[] { "Customer"};
            var customCommands = new List<string>();
            var initializer = new CreateTablesIfNotExist<TmcObjectContext>(tablesToValidate, customCommands.ToArray());
            Database.SetInitializer(initializer);
        }

        public void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        public bool StoredProceduredSupported
        {
            get { return true; }
        }

        public System.Data.Common.DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}
