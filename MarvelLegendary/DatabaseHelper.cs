using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary
{
    public class DatabaseHelper
    {
        string connectionString;
        SqlConnection connection;

        public DatabaseHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MarvelLegendary.Database"].ConnectionString;
            Console.WriteLine(ConfigurationManager.ConnectionStrings["MarvelLegendary.Database"].ConnectionString);
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

        public string GetResult(string sqlString)
        {
            var returnValue = "";

            using (connection)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    // ExecuteScalar will return the first column of the first row as an object
                    var result = command.ExecuteScalar();

                    // Check if the result is not null and is convertible to the expected type
                    if (result != null && result != DBNull.Value)
                    {
                        returnValue = result.ToString();
                    }
                    else
                    {
                        Console.WriteLine("No result found.");
                    }
                }
            }

            return returnValue;
        }

        public string GetResult(string sqlString, Dictionary<string, object> parameters = null)
        {
            var returnValue = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        returnValue = result.ToString();
                    }
                    else
                    {
                        Console.WriteLine("No result found.");
                    }
                }
            }

            return returnValue;
        }

        //public bool DoesExistInByTable(string byTable, string firstId, string secondId)
        //{
        //    var returnValue = false;
        //
        //    using (connection)
        //    {
        //        connection.Open();
        //
        //        
        //        using (SqlCommand command = new SqlCommand(sqlString, connection))
        //        {
        //            // ExecuteScalar will return the first column of the first row as an object
        //            var result = command.ExecuteScalar();
        //
        //            // Check if the result is not null and is convertible to the expected type
        //            if (result != null && result != DBNull.Value)
        //            {
        //                returnValue = result.ToString();
        //            }
        //            else
        //            {
        //                Console.WriteLine("No result found.");
        //            }
        //        }
        //    }
        //
        //    return returnValue;
        //}

        public void InsertInto(string sqlString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }

        public List<string> GetListFromByTable(string tablePrefix, string tableSuffix, string cardName)
        {
            //tablePrefix and tableSuffix can be Henchmen, Scheme, Hero, Villain, or Mastermind
            var prefixTableName = (tablePrefix == "Henchmen") ? "Henchmen" : (tablePrefix == "Hero" ? "Heroes" : $"{tablePrefix}s");
            var suffixTableName = (tableSuffix == "Henchmen") ? "Henchmen" : (tableSuffix == "Hero" ? "Heroes" : $"{tableSuffix}s");
            var byTable = $"{tablePrefix}By{tableSuffix}";
            var tablePrefixId = $"{tablePrefix}Id";

            //This will escape the single quote if it is in the name of the card
            var updatedCardName = cardName.Contains("'") ? cardName.Replace("'", "''") : cardName;

            //need to handle if this is, for example, MastermindxMastermind
            //{tableSuffix}Id has to become {tableSuffix}2Id
            var tableSuffixId = tablePrefix == tableSuffix ? $"{tableSuffix}2Id" : $"{tableSuffix}Id";

            var allItemsBy = $@"select pt.{tablePrefix}Name from {prefixTableName} pt
                    inner join {byTable} bt ON pt.Id = bt.{tablePrefixId}
                    inner join {suffixTableName} st ON st.Id = bt.{tableSuffixId}
                    where st.{tableSuffix}Name = '{updatedCardName}'";

            var allItemsByX = new DatabaseHelper().GetList(allItemsBy);
            return allItemsByX;
        }
    }
}
