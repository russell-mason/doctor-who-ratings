namespace DoctorWhoRatings.Data.Excel;

/// <summary>
/// A helper class that simplifies reading multiple cell values from the same row via a header mapping key.
/// </summary>
/// <param name="spreadsheetReader">The reader that provides access to row and cell values.</param>
/// <param name="row">The row to extract multiple values from.</param>
/// <param name="columnMappings">All mappings between header keys and column references.</param>
public class ExcelCellReader(IExcelSpreadsheetReader spreadsheetReader, Row row, IEnumerable<ExcelColumnMapping> columnMappings)
{
    public T Read<T>(string key)
    {
        var value = spreadsheetReader.GetCellValue<T>(row, columnMappings, key);

        return value;
    }
}
