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
        var writers = ReadWriters().ToList().AsReadOnly();
        var wikiFormats = ReadWikiFormats().ToList().AsReadOnly();
        var populations = ReadPopulation().ToList().AsReadOnly();

        var doctorWhoData = new DoctorWhoData
        {
            Episodes = episodes,
            Doctors = doctors,
            SeasonFormats = seasonFormats,
            EpisodeFormats = episodeFormats,
            Eras = eras,
            Writers = writers,
            WikiFormats = wikiFormats,
            Populations = populations
        };

        return doctorWhoData;
    }

    private IEnumerable<T> ReadRows<T>(string sheetName, Func<Row, IReadOnlyList<ExcelColumnMapping>, T> creationCallback) where T : class
    {
        var rows = spreadsheetReader.ReadRows(sheetName).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var filteredRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(1));

        var createdItems = filteredRows.Select(episodeRow => creationCallback(episodeRow, columnMappings));

        return createdItems;
    }

    private IEnumerable<Episode> ReadEpisodes() => ReadRows(DoctorWhoExcelSheetNames.Episodes, CreateEpisode);

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
            WriterIds = [.. cellReader.Read<string>(nameof(Episode.WriterIds)).Split(",").Select(id => Convert.ToInt32(id))],
            Writers = cellReader.Read<string>(nameof(Episode.Writers)).Split(","),
            WriterAliasId = cellReader.Read<int?>(nameof(Episode.WriterAliasId)),
            WriterAlias = cellReader.Read<string>(nameof(Episode.WriterAlias)),
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

    private IEnumerable<Doctor> ReadDoctors() => ReadRows(DoctorWhoExcelSheetNames.Doctors, CreateDoctor);

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

    private IEnumerable<SeasonFormat> ReadSeasonFormats() => ReadRows(DoctorWhoExcelSheetNames.SeasonFormats, CreateSeasonFormat);

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

    private IEnumerable<EpisodeFormat> ReadEpisodeFormats() => ReadRows(DoctorWhoExcelSheetNames.EpisodeFormats, CreateEpisodeFormat);

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

    private IEnumerable<Era> ReadEras() => ReadRows(DoctorWhoExcelSheetNames.Eras, CreateEra);

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

    private IEnumerable<Writer> ReadWriters() => ReadRows(DoctorWhoExcelSheetNames.Writers, CreateWriter);

    private Writer CreateWriter(Row row, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(row, columnMappings);

        var writer = new Writer
        {
            Id = cellReader.Read<int>(nameof(Writer.Id)),
            Name = cellReader.Read<string>(nameof(Writer.Name)),
            IsAlias = Convert.ToBoolean(cellReader.Read<int?>(nameof(Writer.IsAlias)) ?? 0),
            Note = cellReader.Read<string>(nameof(Writer.Note))
        };

        return writer;
    }

    private IEnumerable<WikiFormat> ReadWikiFormats() => ReadRows(DoctorWhoExcelSheetNames.WikiFormats, CreateWikiFormat);

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

    private IEnumerable<YearPopulation> ReadPopulation() => ReadRows(DoctorWhoExcelSheetNames.Populations, CreateYearPopulation);

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