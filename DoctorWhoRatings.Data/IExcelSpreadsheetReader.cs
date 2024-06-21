namespace DoctorWhoRatings.Data;

/// <summary>
/// Represents the means by which data is read from an Excel spreadsheet file.
/// </summary>
public interface IExcelSpreadsheetReader
{
    /// <summary>
    /// Opens the spreadsheet document located at the specified path.
    /// <para>
    /// This should be disposed of by the consumer.
    /// .</para>
    /// </summary>
    /// <param name="path">The path to the Excel spreadsheet file.</param>
    /// <returns>An OpenXml spreadsheet document.</returns>
    SpreadsheetDocument OpenSpreadsheetDocument(string path);

    /// <summary>
    /// Reads all rows from the sheet with the specified name.
    /// </summary>
    /// <param name="sheetName">The name of the sheet within the spreadsheet to read data from.</param>
    /// <returns>A collection of OpenXml rows.</returns>
    IEnumerable<Row> ReadRows(string sheetName);

    /// <summary>
    /// Returns a collection of mappings between the text within a cell, and it's column reference.
    /// <para>
    /// Intended to provide simple access to named columns provided by a header row. In order to save resources,
    /// Excel does not do a grid mapping, so empty cells are not included. Cells are accessed via a reference,
    /// e.g. A1, so this maps a header such as "Id" to the column reference such as "A"</para>
    /// </summary>
    /// <param name="row">The row that contains the column headers.</param>
    /// <returns>A collection of key/column reference mappings.</returns>
    IEnumerable<ExcelColumnMapping> ReadColumnHeaders(Row? row);

    /// <summary>
    /// Returns a collection of rows that have at least one cell value in them.
    /// </summary>
    /// <param name="rows">The collection of rows to filter.</param>
    /// <returns>A collection of OpenXml rows that are not empty.</returns>
    IEnumerable<Row> FilterOutEmptyRows(IEnumerable<Row> rows);

    /// <summary>
    /// Gets the value of a cell in the specified row, identified by header key, and cast to the specified data type.
    /// </summary>
    /// <typeparam name="T">The type of data to convert the cell value to.</typeparam>
    /// <param name="row">The row that contains the cell to get the value from.</param>
    /// <param name="columnMappings">All mappings between header keys and column references.</param>
    /// <param name="key">The name of the header key to identify the cell by.</param>
    /// <returns>The cell value cast to the specified type.</returns>
    T GetCellValue<T>(Row row, IEnumerable<ExcelColumnMapping> columnMappings, string key);

    /// <summary>
    /// Creates a helper that simplifies reading multiple cell values from the same row via a header mapping key.
    /// </summary>
    /// <param name="episodeRow">The row to provide access to.</param>
    /// <param name="columnMappings">All mappings between header keys and column references.</param>
    /// <returns>A reader linked to the specified row and mappings.</returns>
    ExcelCellReader CreateCellReader(Row episodeRow, IReadOnlyList<ExcelColumnMapping> columnMappings);
}