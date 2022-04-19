using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AITR.Utils
{
    public class Utils
    {
        private static string connectionString;

        private static string targetConnection = ConfigurationManager.ConnectionStrings["CurrentConnection"].ConnectionString;

        public static String GetConnectionString()  {
            if (targetConnection.Equals("dev"))
            {
                connectionString = AppConstant.DevConnectionString;
            }
            else if (targetConnection.Equals("test"))
            {
                connectionString = AppConstant.TestConnectionString;
            }
            else if (targetConnection.Equals("prod"))
            {
                connectionString = AppConstant.ProdConnectionString;
            }
            else
            {
                throw new Exception();

            }
            return connectionString;
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            return conn;
        }   
    }        
}