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
        _doctorWhoData = new Lazy<DoctorWhoData>(GetDoctorWhoData);
    }

    public DoctorWhoData DoctorWhoData => _doctorWhoData.Value;

    public DoctorWhoData GetDoctorWhoData()
    {
        var excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\data", "DoctorWhoRatings.xlsx");
        var doctorWhoData = _doctorWhoDataReader.Read(excelFilePath);

        return doctorWhoData;
    }
}