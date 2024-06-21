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

        var doctorWhoData = new DoctorWhoData
        {
            Episodes = episodes
        };

        return doctorWhoData;
    }

    private IEnumerable<Episode> ReadEpisodes()
    {
        var rows = spreadsheetReader.ReadRows(DoctorWhoExcelSheetNames.Episodes).ToList();
        var columnMappings = spreadsheetReader.ReadColumnHeaders(rows.FirstOrDefault()).ToList().AsReadOnly();
        var episodeRows = spreadsheetReader.FilterOutEmptyRows(rows.Skip(2));

        var episodes = episodeRows.Select(episodeRow => CreateEpisode(episodeRow, columnMappings));
        
        return episodes;
    }

    private Episode CreateEpisode(Row episodeRow, IReadOnlyList<ExcelColumnMapping> columnMappings)
    {
        var cellReader = spreadsheetReader.CreateCellReader(episodeRow, columnMappings);

        var episode = new Episode
        {
            Id = cellReader.Read<int>(nameof(Episode.Id)),
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
            OvernightRatings = cellReader.Read<decimal?>(nameof(Episode.OvernightRatings)),
            ConsolidatedRatings = cellReader.Read<decimal?>(nameof(Episode.ConsolidatedRatings)),
            ExtendedRatings = cellReader.Read<decimal?>(nameof(Episode.ExtendedRatings)),
            PopulationAdjustedOvernightRatings = cellReader.Read<decimal?>(nameof(Episode.PopulationAdjustedOvernightRatings)),
            PopulationAdjustedConsolidatedRatings = cellReader.Read<decimal?>(nameof(Episode.PopulationAdjustedConsolidatedRatings)),
            PopulationAdjustedExtendedRatings = cellReader.Read<decimal?>(nameof(Episode.PopulationAdjustedExtendedRatings)),
            Population = cellReader.Read<int>(nameof(Episode.Population)),
            PopulationFactor = cellReader.Read<decimal>(nameof(Episode.PopulationFactor)),
            Note = cellReader.Read<string>(nameof(Episode.Note))
        };

        return episode;
    }
}