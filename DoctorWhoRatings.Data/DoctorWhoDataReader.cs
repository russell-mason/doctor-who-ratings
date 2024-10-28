namespace DoctorWhoRatings.Data;

/// <summary>
/// Reads all data from an Excel spreadsheet and creates a root dataset.
/// </summary>
/// <param name="spreadsheetReader">A reader capable of extracting values from an Excel spreadsheet.</param>
public class DoctorWhoDataReader(IExcelSpreadsheetReader spreadsheetReader) : IDoctorWhoDataReader
{
    public DoctorWhoData Read(string path)
    {
        using var spreadsheetDocument = spreadsheetReader.OpenSpreadsheetDocument(path);

        var episodes = ReadEpisodes().ToList().AsReadOnly();
        var doctors = ReadDoctors().ToList().AsReadOnly();
        var populations = ReadPopulation().ToList().AsReadOnly();

        var doctorWhoData = new DoctorWhoData
        {
            Episodes = episodes,
            Doctors = doctors,
            Populations = populations
        };

        return doctorWhoData;
    }

    private IEnumerable<Episode> ReadEpisodes()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.Episodes).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var episodeRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var episodes = episodeRows.Select(episodeRow => CreateEpisode(episodeRow, columnMappings));

        return episodes;
    }

    private Episode CreateEpisode(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var episode = new Episode
        {
            Id = cellReader.Read<int>(nameof(Episode.Id)),
            EraId = cellReader.Read<int>(nameof(Episode.EraId)),
            EraDescription = cellReader.Read<string>(nameof(Episode.EraDescription)),
            EpisodeFormatId = cellReader.Read<int?>(nameof(Episode.EpisodeFormatId)),
            EpisodeFormatDescription = cellReader.Read<string?>(nameof(Episode.EpisodeFormatDescription)),
            Doctor = cellReader.Read<int>(nameof(Episode.Doctor)),
            Actor = cellReader.Read<string>(nameof(Episode.Actor)),
            Season = cellReader.Read<int?>(nameof(Episode.Season)),
            SeasonFormatId = cellReader.Read<int>(nameof(Episode.SeasonFormatId)),
            SeasonFormatDescription = cellReader.Read<string>(nameof(Episode.SeasonFormatDescription)),
            Story = cellReader.Read<int>(nameof(Episode.Story)),
            StoryInSeason = cellReader.Read<int?>(nameof(Episode.StoryInSeason)),
            StoryTitle = cellReader.Read<string>(nameof(Episode.StoryTitle)),
            PartInStory = cellReader.Read<int>(nameof(Episode.PartInStory)),
            PartTitle = cellReader.Read<string>(nameof(Episode.PartTitle)),
            OriginalAirDate = cellReader.Read<DateTime>(nameof(Episode.OriginalAirDate)),
            // Missing episodes use an estimate, recorded as a negative value to distinguish them from known runtimes
            Runtime = Math.Abs(cellReader.Read<int>(nameof(Episode.Runtime))),  
            OvernightRatings = cellReader.Read<decimal?>(nameof(Episode.OvernightRatings)),
            ConsolidatedRatings = cellReader.Read<decimal?>(nameof(Episode.ConsolidatedRatings)),
            ExtendedRatings = cellReader.Read<decimal?>(nameof(Episode.ExtendedRatings)),
            Population = cellReader.Read<int>(nameof(Episode.Population)),
            PopulationFactor = cellReader.Read<decimal>(nameof(Episode.PopulationFactor)),
            Note = cellReader.Read<string>(nameof(Episode.Note)),
            WikiUrl = cellReader.Read<string>(nameof(Episode.WikiUrl))
        };

        return episode;
    }

    private IEnumerable<Doctor> ReadDoctors()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.Doctors).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var doctorRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var doctors = doctorRows.Select(doctorRow => CreateDoctor(doctorRow, columnMappings));

        return doctors;
    }

    private Doctor CreateDoctor(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var doctor = new Doctor
        {
            Number = cellReader.Read<int>(nameof(Doctor.Number)),
            Actor = cellReader.Read<string>(nameof(Doctor.Actor))
        };

        return doctor;
    }

    private IEnumerable<YearPopulation> ReadPopulation()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.Populations).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var populationRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var populations = populationRows.Select(populationRow => CreateYearPopulation(populationRow, columnMappings));

        return populations;
    }

    private YearPopulation CreateYearPopulation(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var yearPopulation = new YearPopulation
        {
            Year = cellReader.Read<int>(nameof(YearPopulation.Year)),
            Population = cellReader.Read<int>(nameof(YearPopulation.Population)),
            Note = cellReader.Read<string?>(nameof(YearPopulation.Note))
        };

        return yearPopulation;
    }
}