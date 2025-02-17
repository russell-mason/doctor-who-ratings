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
        _doctorWhoData = new Lazy<DoctorWhoData>(ReadDoctorWhoData);
    }

    public DoctorWhoData DoctorWhoData => _doctorWhoData.Value;

    public void Load()
    {
        _ = DoctorWhoData;  // Use consistent access to ensure lazy loading is invoked
    }

    public void Save()
    {
        WriteDoctorWhoData();
    }

    private DoctorWhoData ReadDoctorWhoData()
    {
        var excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\data", "DoctorWhoRatings.xlsx");
        var doctorWhoData = _doctorWhoDataReader.Read(excelFilePath);

        return doctorWhoData;
    }

    private void WriteDoctorWhoData()
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\data", "doctor-who-ratings.json");

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(DoctorWhoData, options);

        File.WriteAllText(jsonFilePath, json);
    }
}