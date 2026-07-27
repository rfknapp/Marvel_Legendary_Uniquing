using ClosedXML.Excel;
using System.Data;
using System.Linq;

namespace MarvelLegendary.Exclusions
{
    public class GetSpreadsheet
    {
        public EnumerableRowCollection<DataRow> GetSpreadsheetInfo(string tabName)
        {
            var spreadsheetName = "Marvel_Legendary_Every_Combo.xlsx";
            var filePath = $@"..\..\Resources\{spreadsheetName}";

            var table = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(tabName);

                var range = worksheet.RangeUsed();

                // Create columns
                for (int i = 0; i < range.ColumnCount(); i++)
                {
                    table.Columns.Add($"Column{i}");
                }

                // Add rows
                foreach (var row in range.Rows())
                {
                    var dataRow = table.NewRow();

                    for (int i = 0; i < range.ColumnCount(); i++)
                    {
                        dataRow[i] = row.Cell(i + 1).Value;
                    }

                    table.Rows.Add(dataRow);
                }
            }

            return table.AsEnumerable();
        }
    }
}
