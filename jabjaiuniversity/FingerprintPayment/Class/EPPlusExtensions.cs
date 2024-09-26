using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
{
    public static class EPPlusExtensions
    {
        /// <summary>
        /// Extracts a DataSet from the ExcelPackage.
        /// </summary>
        /// <param name="package">The Excel package.</param>
        /// <param name="firstRowContainsHeader">if set to <c>true</c> [first row contains header].</param>
        /// <returns></returns>
        public static DataSet ToDataSet(this ExcelPackage package, bool firstRowContainsHeader = false)
        {
            var headerRow = firstRowContainsHeader ? 1 : 0;
            return ToDataSet(package, headerRow);
        }

        /// <summary>
        /// Extracts a DataSet from the ExcelPackage.
        /// </summary>
        /// <param name="package">The Excel package.</param>
        /// <param name="headerRow">The header row. Use 0 if there is no header row. Value must be 0 or greater.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">headerRow must be 0 or greater.</exception>
        public static DataSet ToDataSet(this ExcelPackage package, int headerRow = 0)
        {
            if (headerRow < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(headerRow), headerRow, "Must be 0 or greater.");
            }

            var result = new DataSet();

            foreach (var sheet in package.Workbook.Worksheets)
            {
                var table = new DataTable { TableName = sheet.Name };

                int sheetStartRow = 1;
                if (headerRow > 0)
                {
                    sheetStartRow = headerRow;
                }

                if (sheet.Dimension == null) continue;
                var columns = from firstRowCell in sheet.Cells[sheetStartRow, 1, sheetStartRow, sheet.Dimension.End.Column]
                              where firstRowCell.Value != null
                              select new DataColumn(headerRow > 0 ? firstRowCell.Value.ToString().Trim() : $"Column {firstRowCell.Start.Column}");
                if (columns.Count() == 0) continue;
                table.Columns.AddRange(columns.ToArray());

                var startRow = headerRow > 0 ? sheetStartRow + 1 : sheetStartRow;

                for (var rowIndex = startRow; rowIndex <= sheet.Dimension.End.Row; rowIndex++)
                {
                    var inputRow = sheet.Cells[rowIndex, 1, rowIndex, sheet.Dimension.End.Column];
                    var row = table.Rows.Add();

                    foreach (var cell in inputRow)
                    {
                        if (cell.Start.Column <= columns.Count())
                        {
                            row[cell.Start.Column - 1] = cell.Value;
                        }
                    }

                    if (row[0] == null || row[columns.Count() - 1] == null) break;
                }

                result.Tables.Add(table);
            }

            return result;
        }


        /// <summary>
        /// Extracts a DataSet from the ExcelPackage.
        /// </summary>
        /// <param name="package">The Excel package.</param>
        /// <param name="sheetNames">Sheet name list</param>
        /// <param name="headerRows">The header rows. Use 0 if there is no header row. Value must be 0 or greater.</param>
        /// <returns></returns>
        public static DataSet ToDataSet(this ExcelPackage package, string[] sheetNames, int[] headerRows)
        {
            var result = new DataSet();

            int sheetIndex = 0;
            foreach (var sheet in package.Workbook.Worksheets)
            {
                int isInList = Array.IndexOf(sheetNames, sheet.Name);
                if (isInList == -1) continue;

                var table = new DataTable { TableName = sheet.Name };

                int sheetStartRow = 1;
                if (headerRows[sheetIndex] > 0)
                {
                    sheetStartRow = headerRows[sheetIndex];
                }

                if (sheet.Dimension == null) continue;
                //var columns = from firstRowCell in sheet.Cells[sheetStartRow, 1, sheetStartRow, sheet.Dimension.End.Column]
                //              where firstRowCell.Value != null
                //              select new DataColumn(headerRows[sheetIndex] > 0 ? $"{firstRowCell.Value.ToString().Trim()} {firstRowCell.Start.Column}" : $"Column {firstRowCell.Start.Column}");
                var columns = from firstRowCell in sheet.Cells[sheetStartRow, 1, sheetStartRow, sheet.Dimension.End.Column]
                              select new DataColumn(firstRowCell.Value == null ? firstRowCell.Start.Address : firstRowCell.Value.ToString().Trim() + $" {firstRowCell.Start.Column}");
                if (columns.Count() == 0) continue;
                table.Columns.AddRange(columns.ToArray());

                var startRow = headerRows[sheetIndex] > 0 ? sheetStartRow + 1 : sheetStartRow;

                for (var rowIndex = startRow; rowIndex <= sheet.Dimension.End.Row; rowIndex++)
                {
                    var inputRow = sheet.Cells[rowIndex, 1, rowIndex, sheet.Dimension.End.Column];
                    var row = table.Rows.Add();

                    foreach (var cell in inputRow)
                    {
                        if (cell.Start.Column <= columns.Count())
                        {
                            if (cell.Value?.GetType() == typeof(DateTime))
                            {
                                row[cell.Start.Column - 1] = ((DateTime)cell.Value).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                row[cell.Start.Column - 1] = cell.Value;
                            }
                        }
                    }

                    if (row[0] == null || row[columns.Count() - 1] == null) break;
                }

                result.Tables.Add(table);

                sheetIndex++;
            }

            return result;
        }

    }
}