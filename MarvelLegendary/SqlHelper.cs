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

        public bool DoesExistInByTable(string byTable, string firstId, string secondId)
        {
            var returnValue = false;

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

        public void InsertInto(string sqlString)
        {
            SqlTransaction transaction;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    try
                    {
                        // Execute SQL INSERT operation within the transaction
                        SqlCommand command = new SqlCommand(sqlString, connection, transaction);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");

                        // Commit the transaction if all operations are successful
                        transaction.Commit();
                        Console.WriteLine("Transaction committed successfully.");
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction if any operation fails
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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

            var allItemsByX = new SqlHelper().GetList(allItemsBy);
            return allItemsByX;
        }
    }
}
