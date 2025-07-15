using System.Data;
using OfficeOpenXml;

namespace DemoMVC.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string strPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fi = new FileInfo(strPath);
            using var excelPackage = new ExcelPackage(fi);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            DataTable dt = new DataTable();

            if (worksheet.Dimension == null)
                return dt;

            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;

            for (int col = 1; col <= colCount; col++)
            {
                string colName = worksheet.Cells[1, col].Text.Trim();
                if (string.IsNullOrEmpty(colName))
                    colName = $"Column{col}";

                if (dt.Columns.Contains(colName))
                    colName += $"_{col}";

                dt.Columns.Add(colName);
            }

            for (int row = 2; row <= rowCount; row++)
            {
                DataRow newRow = dt.NewRow();
                for (int col = 1; col <= colCount; col++)
                {
                    newRow[col - 1] = worksheet.Cells[row, col].Text;
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }
    }
}
