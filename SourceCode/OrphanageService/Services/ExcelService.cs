using OrphanageService.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrphanageService.Services
{
    public class ExcelService : IExcelService
    {
        private readonly ILogger _logger;

        public ExcelService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> ConvertToXlsx(IDictionary<string, IList<string>> data, CancellationToken cancellationToken)
        {
            return await Task.Factory.StartNew(() =>
            {
                _logger.Information("trying to convert lists of data to xlsx file");
                _logger.Information("trying to open Excel Work Book.");
                ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
                _logger.Information("trying to add Excel Work Sheet.");
                var sheet = wbook.Worksheets.Add("sheet1");
                int row = 1;
                int col = 1;
                int rowsCount = data[data.Keys.First()].Count;
                int columnsCount = data.Keys.Count;
                var keys = data.Keys.ToArray();
                _logger.Information($"start printing data to excel file, ColumnsCount={columnsCount} . RowsCount={rowsCount}");
                for (row = 1; row <= rowsCount + 1; row++)
                {
                    for (col = 1; col <= columnsCount; col++)
                    {
                        string key = keys[col - 1];
                        var values = data[key];

                        if (row == 1)
                        {
                            //write the column header
                            sheet.Cell(1, col).Value = key;
                            sheet.Cell(1, col).Style.Font.Bold = true;
                            sheet.Cell(1, col).Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                            sheet.Cell(1, col).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                            sheet.Cell(1, col).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                            sheet.Cell(1, col).Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.Black;
                        }
                        else
                        {
                            sheet.Cell(row, col).Value = values[row - 2];
                        }
                    }
                    if (cancellationToken != null && cancellationToken.IsCancellationRequested)
                    {
                        _logger.Information($"the operation has broken, a cancellation request has been sent.");
                        break;
                    }
                }
                _logger.Information($"all data has been printed successfully to the excel file.");
                sheet.ExpandColumns();
                var rang = sheet.Range(1, 1, rowsCount + 1, columnsCount);
                rang.Style.Border.SetOutsideBorderColor(ClosedXML.Excel.XLColor.Black);
                rang.Style.Border.SetOutsideBorder(ClosedXML.Excel.XLBorderStyleValues.Thin);
                rang.Style.Border.SetInsideBorder(ClosedXML.Excel.XLBorderStyleValues.Thin);
                wbook.Author = "Orphanage Service V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wbook.SaveAs(memoryStream);
                    _logger.Information($"the excel file has been saved successfully, an array of bytes will be returned.");
                    return memoryStream.ToArray();
                }
            });
        }

        public async Task<byte[]> ConvertToXlsx(IDictionary<string, IList<string>> data)
        {
            return await Task.Factory.StartNew(() =>
            {
                _logger.Information("trying to convert lists of data to xlsx file");
                _logger.Information("trying to open Excel Work Book.");
                ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
                _logger.Information("trying to add Excel Work Sheet.");
                var sheet = wbook.Worksheets.Add("sheet1");
                int row = 1;
                int col = 1;
                int rowsCount = data[data.Keys.First()].Count;
                int columnsCount = data.Keys.Count;
                var keys = data.Keys.ToArray();
                _logger.Information($"start printing data to excel file, ColumnsCount={columnsCount} . RowsCount={rowsCount}");
                for (row = 1; row <= rowsCount + 1; row++)
                {
                    for (col = 1; col <= columnsCount; col++)
                    {
                        string key = keys[col - 1];
                        var values = data[key];

                        if (row == 1)
                        {
                            //write the column header
                            sheet.Cell(1, col).Value = key;
                            sheet.Cell(1, col).Style.Font.Bold = true;
                            sheet.Cell(1, col).Style.Font.FontColor = ClosedXML.Excel.XLColor.White;
                            sheet.Cell(1, col).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                            sheet.Cell(1, col).Style.Border.SetOutsideBorder(ClosedXML.Excel.XLBorderStyleValues.Thin);
                            sheet.Cell(1, col).Style.Border.SetOutsideBorderColor(ClosedXML.Excel.XLColor.Black);
                        }
                        else
                        {
                            sheet.Cell(row, col).Value = values[row - 2];
                        }
                    }
                }
                _logger.Information($"all data has been printed successfully to the excel file.");
                sheet.ExpandColumns();
                var rang = sheet.Range(1, 1, rowsCount + 1, columnsCount);
                rang.Style.Border.SetOutsideBorderColor(ClosedXML.Excel.XLColor.Black);
                rang.Style.Border.SetOutsideBorder(ClosedXML.Excel.XLBorderStyleValues.Thin);
                rang.Style.Border.SetInsideBorder(ClosedXML.Excel.XLBorderStyleValues.Thin);
                rang.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                rang.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                wbook.Author = "Orphanage Service V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wbook.SaveAs(memoryStream);
                    _logger.Information($"the excel file has been saved successfully, an array of bytes will be returned.");
                    return memoryStream.ToArray();
                }
            });
        }

        public async Task<FileContentResult> ConvertToXlsxFile(IDictionary<string, IList<string>> data, CancellationToken cancellationToken)
        {
            var excelData = await ConvertToXlsx(data, cancellationToken);
            return new System.Web.Mvc.FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<FileContentResult> ConvertToXlsxFile(IDictionary<string, IList<string>> data)
        {
            var excelData = await ConvertToXlsx(data);
            return new System.Web.Mvc.FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}