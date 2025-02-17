namespace DoctorWhoRatings.Data.Excel;

/// <summary>
/// Provides a general mechanism for reading data from an Excel spreadsheet.
/// N.B. Excel can contain rounding errors in data when read through cell values.
/// e.g. 4.4 may be read as 4.4000000000000004
/// These values will be read into the model as is, so need to be corrected through formatting. 
/// </summary>
public partial class ExcelSpreadsheetReader() : IExcelSpreadsheetReader
{
    [GeneratedRegex(@"(?i)^(?<column>[a-z]+)(?<row>[0-9]+)$", RegexOptions.Compiled)]
    private static partial Regex CellReferenceRegex();

    private SpreadsheetDocument? SpreadsheetDocument { get; set; }

    public SpreadsheetDocument OpenSpreadsheetDocument(string path)
    {
        SpreadsheetDocument = SpreadsheetDocument.Open(path, false);

        if (SpreadsheetDocument.WorkbookPart?.SharedStringTablePart?.SharedStringTable == null)
        {
            throw new InvalidOperationException("Spreadsheet does not contain the required SharedStringTable");
        }

        if (SpreadsheetDocument.WorkbookPart == null)
        {
            throw new InvalidOperationException("Spreadsheet does not contain the required WorkbookPart");
        }

        return SpreadsheetDocument;
    }

    public IEnumerable<Row> ReadRows(string sheetName)
    {
        if (SpreadsheetDocument == null)
        {
            throw new InvalidOperationException("SpreadsheetDocument is not open");
        }

        var sheet = SpreadsheetDocument.WorkbookPart!.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName)
                    ?? throw new ArgumentException($"Sheet '{sheetName}' not found", nameof(sheetName));

        var sheetId = sheet.Id?.Value
                      ?? throw new InvalidOperationException($"Sheet '{sheetName}' does not contain the required Id");

        if (SpreadsheetDocument.WorkbookPart.GetPartById(sheetId) is not WorksheetPart worksheetPart)
        {
            throw new InvalidOperationException($"Sheet '{sheetName}' does not contain the required WorksheetPart");
        }

        var rows = worksheetPart.Worksheet.Descendants<Row>();

        return rows;
    }

    public IEnumerable<ExcelColumnMapping> ReadColumnHeaders(Row? row)
    {
        List<ExcelColumnMapping> columnMappings = [];

        if (row != null)
        {
            columnMappings = row.Elements<Cell>().Select(GetColumnMapping).ToList();
        }

        return columnMappings;
    }

    public IEnumerable<Row> FilterOutEmptyRows(IEnumerable<Row> rows)
    {
        return rows.Where(row => !IsEmpty(row));
    }

    public T GetCellValue<T>(Row row, IEnumerable<ExcelColumnMapping> columnMappings, string key)
    {
        var columnMapping = columnMappings.FirstOrDefault(c => c.Key == key)
                            ?? throw new ArgumentException($"Column '{key}' not found", nameof(key));

        var cellReference = $"{columnMapping.ColumnReference}{row.RowIndex}";
        var cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
        var cellText = GetCellText(cell);

        var dataType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        var cellValue = string.IsNullOrEmpty(cellText) ? default(T) : Convert.ChangeType(cellText, dataType);
        var value = (T) cellValue!;

        return value;
    }

    public ExcelCellReader CreateCellReader(Row episodeRow, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = new ExcelCellReader(this, episodeRow, columnMappings);

        return cellReader;
    }

    private string? GetCellText(Cell? cell)
    {
        if (cell?.CellValue?.Text == null)
        {
            return null;
        }

        if (cell.DataType == null || cell.DataType != CellValues.SharedString)
        {
            return cell.CellValue.Text;
        }

        var sharedStringId = int.Parse(cell.CellValue.Text);
        var text = SpreadsheetDocument?.WorkbookPart?.SharedStringTablePart?.SharedStringTable.ChildElements[sharedStringId].InnerText;

        return text;
    }

    private bool IsEmpty(Row row) => row.Elements<Cell>().All(cell => GetCellText(cell) == null);

    private ExcelColumnMapping GetColumnMapping(Cell cell)
    {
        var key = GetCellText(cell)
                  ?? throw new InvalidOperationException($"Cell ${cell.CellReference} does not contain a value");

        var columnReference = GetColumnReference(cell);
        var excelHeaderCell = new ExcelColumnMapping(key, columnReference);

        return excelHeaderCell;
    }

    private static string GetColumnReference(Cell cell)
    {
        var cellReference = cell.CellReference?.Value
                            ?? throw new InvalidOperationException("Cell does not contain a reference");

        var match = CellReferenceRegex().Match(cellReference);
        var columnValue = match.Groups["column"].Value;

        return columnValue;
    }
}