using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace Yis.Framework.Data.Database
{
    public sealed class DbDataParamHelper
    {
        private DbDataParamHelper()
        {
        }

        public static IDataParameter BuildDataParameter(IDbConnection connection, DbType dataType, string parameterName, object value)
        {
            IDataParameter param = default(IDataParameter);

            switch (connection.GetType().FullName.ToUpper())
            {
                case "SYSTEM.DATA.SQLCLIENT.SQLCONNECTION":
                    param = new SqlParameter();
                    parameterName = parameterName.Replace(":", "@");
                    break;

                case "SYSTEM.DATA.ORACLECLIENT.ORACLECONNECTION":
                    param = new OracleParameter();
                    parameterName = parameterName.Replace("@", ":");
                    break;

                case "SYSTEM.DATA.ODBC":
                    param = new OdbcParameter();
                    break;

                default:
                    throw new InvalidOperationException("Ce DataProvider n'est pas connu par le framework");
            }

            param.DbType = dataType;

            //if (connection.GetType().FullName.ToUpper() == "SYSTEM.DATA.ORACLECLIENT.ORACLECONNECTION")
            //{
            //    switch (dataType)
            //    {
            //        case DbType.String:
            //            param.DbType = (DbType) OracleType.VarChar;
            //            break;
            //        case DbType.Int16:
            //            param.DbType = (DbType) OracleType.Int16;
            //            break;
            //        case DbType.Int32:
            //            param.DbType = (DbType) OracleType.Int32;
            //            break;
            //        case DbType.DateTime:
            //            param.DbType = (DbType) OracleType.DateTime;
            //            break;
            //        case DbType.Double:
            //            param.DbType = (DbType) OracleType.Double;
            //            break;
            //    }
            //}

            param.Value = value ?? DBNull.Value;
            param.ParameterName = parameterName;

            return param;
        }
    }
}