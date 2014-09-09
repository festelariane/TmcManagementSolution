using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.ImportExport;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Domain.Transaction;

namespace Tmc.BLL.Impl.ImportExport
{
    public class ExportService : IExportService
    { 
        public void ExportCustomersToXlsx(Stream stream, IList<Customer> customers)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Customers");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "CustomerId",
                        "FullName",
                        "Username",
                        "Points",
                        "CardType",
                        "CreatedOnUtc",
                        "UpdatedOnUtc",
                        "LastActivityDateUtc",
                        "LastLoginDateUtc"
                    };
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                int row = 2;
                foreach (var customer in customers)
                {
                    int col = 1;

                    worksheet.Cells[row, col].Value = customer.Id;
                    col++;

                    worksheet.Cells[row, col].Value = customer.FullName;
                    col++;

                    worksheet.Cells[row, col].Value = customer.UserName;
                    col++;

                    worksheet.Cells[row, col].Value = customer.Points;
                    col++;

                    worksheet.Cells[row, col].Value = customer.CardType != null ? customer.CardType.Name : "";
                    col++;

                    worksheet.Cells[row, col].Value = GetDateString(customer.CreatedOnUtc);
                    col++;

                    worksheet.Cells[row, col].Value = GetDateString(customer.UpdatedOnUtc);
                    col++;

                    worksheet.Cells[row, col].Value = GetDateString(customer.LastActivityDateUtc);
                    col++;

                    worksheet.Cells[row, col].Value = customer.LastLoginDateUtc == null ? string.Empty : GetDateString(customer.LastLoginDateUtc.Value);
                
                    row++;
                }
                // save the new spreadsheet
                xlPackage.Save();
            }
        }

        public void ExportDepositTransactionsToXlsx(Stream stream, IList<DepositTransaction> transactions)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Transactions");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "Customer Name",
                        "Deposit Amount",
                        "Points",
                        "Exchange Rate",
                        "Created On"
                    };
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                int row = 2;
                foreach (var transaction in transactions)
                {
                    int col = 1;

                    worksheet.Cells[row, col].Value = transaction.Customer != null ? transaction.Customer.UserName : "";
                    col++;

                    worksheet.Cells[row, col].Value = transaction.Amount;
                    col++;

                    worksheet.Cells[row, col].Value = transaction.Points;
                    col++;

                    worksheet.Cells[row, col].Value = transaction.ExchangeRate;
                    col++;

                    worksheet.Cells[row, col].Value = GetDateString(transaction.CreatedOnUtc);
                   
                    row++;
                }
                // save the new spreadsheet
                xlPackage.Save();
            }
        }
        
        private string GetDateString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
