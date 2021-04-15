using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NodeCMBAPI.Services
{
    public class DBConnection
    {
        public SqlConnection connection = null;
        public static string connectionString;
        public DBConnection()
        {
            //connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["NodeCMB_Connection"].ConnectionString);

            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = @"SLIPDL1060281N2\MSSQLSERVER2016";
            //builder.UserID = "sa";
            //builder.Password = "123456";
            //builder.InitialCatalog = "NodeCMB";

            //connection = new SqlConnection(builder.ConnectionString);

            connection = new SqlConnection(connectionString);
            openConnection();

        }

        public void openConnection()
        {
            connection.Open();
        }

        public void closeConnection()
        {
            connection.Close();
        }

        public SqlConnection getConnection()
        {
            openConnection();
            return connection;
        }


        public static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);

            return appRoot;
        }
    }
}
