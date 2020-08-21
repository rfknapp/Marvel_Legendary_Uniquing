using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelLegendary.Exclusions
{
    static class GetSpreadsheet
    {
        public static EnumerableRowCollection<DataRow> GetSpreadsheetInfo(string tabName)
        {
            var spreadsheetName = "Marvel_Legendary_Every_Combo.xlsx";
            //var directory = Directory.GetCurrentDirectory();
            //var directoryPath = directory.Split(new string[] { "bin" }, StringSplitOptions.None)[0];
            //var filePath = $"{directoryPath}{spreadsheetName}";
            var filePath = $@"C:\Users\knaro\Documents\{spreadsheetName}";
            //var filePath = $@"..\..\Resources\{spreadsheetName}";
            var connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0; data source={filePath}; Extended Properties=Excel 12.0;";

            var adapter = new OleDbDataAdapter($"SELECT * FROM [{tabName}$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "testTableName");

            var data = ds.Tables["testTableName"].AsEnumerable();
            return data;
        }
    }
}
