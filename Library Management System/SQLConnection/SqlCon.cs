using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Library_Management_System.SQLConnection
{
    public class SqlCon
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["LMSDatabase"].ConnectionString;
            }
        }

        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
    }
}