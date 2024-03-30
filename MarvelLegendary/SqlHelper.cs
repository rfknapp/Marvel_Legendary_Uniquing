using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary
{
    public class SqlHelper
    {
        string connectionString;
        SqlConnection connection;

        public SqlHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MarvelLegendary.Database"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public List<string> GetList(string sqlString)
        {
            List<string> returnList = new List<string>();

            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        returnList.Add(name);
                    }
                }
            }

            return returnList;
        }
    }
}
