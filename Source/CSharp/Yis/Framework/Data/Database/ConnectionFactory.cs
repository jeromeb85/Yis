using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Data.Database
{
    public class ConnectionFactory
    {

        private IDbConnection _dbConnection;
        private string _connectionString;
        private string _providername;

        public ConnectionFactory(string providerName, string connectionString)
        {
            _providername = providerName;
            _connectionString = connectionString;
        }
        
        public IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    switch (_providername.ToUpper())
                    {
                        case "SYSTEM.DATA.SQLCLIENT":
                            _dbConnection = new SqlConnection(_connectionString);
                            break;
                        case "SYSTEM.DATA.ORACLECLIENT":
                            _dbConnection = new OracleConnection(_connectionString);
                            break;
                        case "SYSTEM.DATA.ODBC":
                            _dbConnection = new OdbcConnection(_connectionString);
                            break;
                        default:
                            throw new InvalidOperationException("Ce DataProvider n'est pas connu par le framework");
                    }
                }

                return _dbConnection;
            }
        }
    }

}
