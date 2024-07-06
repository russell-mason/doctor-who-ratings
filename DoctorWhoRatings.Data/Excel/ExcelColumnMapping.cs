namespace DoctorWhoRatings.Data.Excel;

/// <summary>
/// Represents a mapping between a header key and a column reference in an Excel sheet.
/// <para>
/// e.g. a Column Header with the key of "Id" might map to the Excel column reference "A".
/// </para>
/// </summary>
/// <param name="Key">The header key, e.g. "Id".</param>
/// <param name="ColumnReference">The Excel column reference, e.g. "A".</param>
public record ExcelColumnMapping(string Key, string ColumnReference);
