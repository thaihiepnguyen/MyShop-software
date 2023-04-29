using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Excel;
using MyShop.DAO;

namespace MyShop.BUS
{
    class SheetBUS
    {
        enum Product
        {
            ProName,
            Ram,
            Rom,
            ScreenSize,
            TinyDes,
            Price,
            Trademark,
            BatteryCapacity,
            CatID,
            Quantity
        }

        public List<ProductDTO> ReadExcelFile(string filePath)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                WorksheetPart worksheetPart = (WorksheetPart)(workbookPart.GetPartById(sheet.Id));

                SharedStringTablePart sharedStringTablePart;
                if (workbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    return null;
                }

                IEnumerable<Row> rows = worksheetPart.Worksheet.Descendants<Row>();

                foreach (Row row in rows)
                {
                    int columnIndex = 0;
                    ProductDTO productDTO = new ProductDTO();
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        string cellValue = cell.InnerText;

                        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                        {
                            int sharedStringIndex = int.Parse(cellValue);
                            cellValue = sharedStringTablePart.SharedStringTable.ElementAt(sharedStringIndex).InnerText;
                        }
                        else if (cell.DataType != null && cell.DataType.Value == CellValues.String && cell.CellValue != null)
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(cell.CellValue.Text);
                            cellValue = Encoding.UTF8.GetString(bytes);
                        }

                        if (columnIndex == (int)Product.ProName)
                        {
                            productDTO.ProName = cellValue;
                        }
                        if (columnIndex == (int)Product.Ram)
                        {
                            productDTO.Ram = int.Parse(cellValue);
                        }
                        if (columnIndex == (int)Product.Rom)
                        {
                            productDTO.Rom = int.Parse(cellValue);
                        }
                        if (columnIndex == (int)Product.CatID)
                        {
                            productDTO.CatID = int.Parse(cellValue);
                        }
                        if (columnIndex == (int)Product.BatteryCapacity)
                        {
                            productDTO.BatteryCapacity = int.Parse(cellValue);
                        }
                        if (columnIndex == (int)Product.Quantity)
                        {
                            productDTO.Quantity = int.Parse(cellValue);
                        }
                        if (columnIndex == (int)Product.Trademark)
                        {
                            productDTO.Trademark = cellValue;
                        }
                        if (columnIndex == (int)Product.ScreenSize)
                        {
                            productDTO.ScreenSize = double.Parse(cellValue);
                        }
                        if (columnIndex == (int)Product.TinyDes)
                        {
                            productDTO.TinyDes = cellValue;
                        }
                        if (columnIndex == (int)Product.Price)
                        {
                            productDTO.Price = Decimal.Parse(cellValue);
                        }
                        columnIndex++;
                    }
                    result.Add(productDTO);
                }
            }

            return result;
        }
    }
}
