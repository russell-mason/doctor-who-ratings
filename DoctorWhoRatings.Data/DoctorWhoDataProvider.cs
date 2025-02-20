namespace DoctorWhoRatings.Data;

/// <summary>
/// Provides all access to the root Doctor Who dataset, specifically read via an Excel spreadsheet.
/// </summary>
public class DoctorWhoDataProvider : IDoctorWhoDataProvider
{
    private readonly IDoctorWhoDataReader _doctorWhoDataReader;
    private readonly Lazy<DoctorWhoData> _doctorWhoData;

    public DoctorWhoDataProvider(IDoctorWhoDataReader doctorWhoDataReader)
    {
        _doctorWhoDataReader = doctorWhoDataReader;
        _doctorWhoData = new Lazy<DoctorWhoData>(ReadDoctorWhoDataFromExcelFile);
    }

    public DoctorWhoData DoctorWhoData => _doctorWhoData.Value;

    public void Load()
    {
        _ = DoctorWhoData;  // Use consistent access to ensure lazy loading is invoked
    }

    public void Save()
    {
        if (!DoctorWhoDataJsonFileExists())
        {
            WriteDoctorWhoDataToJsonFile();
        }
    }

    private DoctorWhoData ReadDoctorWhoDataFromExcelFile()
    {
        var excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\data", "DoctorWhoRatings.xlsx");
        var doctorWhoData = _doctorWhoDataReader.Read(excelFilePath);

        return doctorWhoData;
    }

    private void WriteDoctorWhoDataToJsonFile()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(DoctorWhoData, options);

        File.WriteAllText(GetJsonFilePath(), json);
    }

    private static bool DoctorWhoDataJsonFileExists() => File.Exists(GetJsonFilePath());

    private static string GetJsonFilePath() => Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\data", "doctor-who-ratings.json");
}