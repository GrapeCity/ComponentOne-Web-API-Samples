using C1.Web.Api.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace WebApi
{
    public class SqlDataProvider : IDataProvider
    {
        public SqlDataProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString
        {
            get;
            private set;
        }

        public IEnumerable Read(NameValueCollection args = null)
        {
            args = args ?? new NameValueCollection();
            var tableName = args["tableName"];
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return null;
            }

            var selectCommand = string.Format("select * from {0}", tableName);
            var conn = new SqlConnection(ConnectionString);
            var command = new SqlCommand(selectCommand) { Connection = conn };
            conn.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}