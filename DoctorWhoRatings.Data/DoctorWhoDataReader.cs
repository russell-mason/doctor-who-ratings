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
        var seasonFormats = ReadSeasonFormats().ToList().AsReadOnly();
        var episodeFormats = ReadEpisodeFormats().ToList().AsReadOnly();
        var eras = ReadEras().ToList().AsReadOnly();
        var wikiFormats = ReadWikiFormats().ToList().AsReadOnly();
        var populations = ReadPopulation().ToList().AsReadOnly();

        var doctorWhoData = new DoctorWhoData
        {
            Episodes = episodes,
            Doctors = doctors,
            SeasonFormats = seasonFormats,
            EpisodeFormats = episodeFormats,
            Eras = eras,
            WikiFormats = wikiFormats,
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
            EpisodeInSeason = cellReader.Read<int?>(nameof(Episode.EpisodeInSeason)),
            OriginalAirDate = cellReader.Read<DateTime>(nameof(Episode.OriginalAirDate)),
            // Unknown runtimes use an estimate, recorded as a negative value to distinguish them from known runtimes
            Runtime = Math.Abs(cellReader.Read<int>(nameof(Episode.Runtime))),
            IsMissing = Convert.ToBoolean(cellReader.Read<int?>(nameof(Episode.IsMissing)) ?? 0),
            OvernightRatings = cellReader.Read<decimal?>(nameof(Episode.OvernightRatings)),
            ConsolidatedRatings = cellReader.Read<decimal?>(nameof(Episode.ConsolidatedRatings)),
            ExtendedRatings = cellReader.Read<decimal?>(nameof(Episode.ExtendedRatings)),
            Population = cellReader.Read<int>(nameof(Episode.Population)),
            PopulationFactor = cellReader.Read<decimal>(nameof(Episode.PopulationFactor)),
            Note = cellReader.Read<string>(nameof(Episode.Note)),
            WikiFormatId = cellReader.Read<int?>(nameof(Episode.WikiFormatId)),
            WikiFormatDescription = cellReader.Read<string?>(nameof(Episode.WikiFormatDescription)),
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

    private IEnumerable<SeasonFormat> ReadSeasonFormats()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.SeasonFormats).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var seasonFormatRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var seasonFormats = seasonFormatRows.Select(seasonFormatRow => CreateSeasonFormat(seasonFormatRow, columnMappings));

        return seasonFormats;
    }

    private SeasonFormat CreateSeasonFormat(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var seasonFormat = new SeasonFormat
        {
            Id = cellReader.Read<int>(nameof(SeasonFormat.Id)),
            Description = cellReader.Read<string>(nameof(SeasonFormat.Description))
        };

        return seasonFormat;
    }

    private IEnumerable<EpisodeFormat> ReadEpisodeFormats()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.EpisodeFormats).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var episodeFormatRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var episodeFormats = episodeFormatRows.Select(episodeFormatRow => CreateEpisodeFormat(episodeFormatRow, columnMappings));

        return episodeFormats;
    }

    private EpisodeFormat CreateEpisodeFormat(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var episodeFormat = new EpisodeFormat
        {
            Id = cellReader.Read<int>(nameof(EpisodeFormat.Id)),
            Description = cellReader.Read<string>(nameof(EpisodeFormat.Description))
        };

        return episodeFormat;
    }

    private IEnumerable<Era> ReadEras()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.Eras).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var eraRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var eras = eraRows.Select(eraRow => CreateEra(eraRow, columnMappings));

        return eras;
    }

    private Era CreateEra(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var era = new Era
        {
            Id = cellReader.Read<int>(nameof(Era.Id)),
            Description = cellReader.Read<string>(nameof(Era.Description))
        };

        return era;
    }

    private IEnumerable<WikiFormat> ReadWikiFormats()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.WikiFormats).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var wikiFormatRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var wikiFormats = wikiFormatRows.Select(eraRow => CreateWikiFormat(eraRow, columnMappings));

        return wikiFormats;
    }

    private WikiFormat CreateWikiFormat(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var wikiFormat = new WikiFormat
        {
            Id = cellReader.Read<int>(nameof(WikiFormat.Id)),
            Description = cellReader.Read<string>(nameof(WikiFormat.Description))
        };

        return wikiFormat;
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