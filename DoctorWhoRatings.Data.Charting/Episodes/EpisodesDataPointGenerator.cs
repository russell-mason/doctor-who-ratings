namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodesDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IEpisodesDataPointGenerator
{
    public List<EpisodeDataPoint> Generate(EpisodesDataOptions options) => Filter(options).Select(CreateDataPoint).ToList();

    public EpisodeDataPoint Generate(Episode episode) => CreateDataPoint(episode);

    public List<Episode> Filter(EpisodesDataOptions options) => [.. dataProvider.DoctorWhoData.Episodes.Where(episode => MatchesFilter(episode, options))];

    private static bool MatchesFilter(Episode episode, EpisodesDataOptions options) =>
        (options.IdFilter == null || episode.Id == options.IdFilter) &&
        (options.SlugFilter == null || episode.Slug == options.SlugFilter) &&
        (options.EraId == null || episode.EraId == options.EraId) &&
        (options.DoctorFilter == null || episode.Doctor == options.DoctorFilter) &&
        (options.SeasonFilter == null || episode.Season == options.SeasonFilter) &&
        (options.StoryFilter == null || episode.Story == options.StoryFilter) &&
        (options.EpisodeFormatIdFilter == null || episode.EpisodeFormatId == options.EpisodeFormatIdFilter) &&
        (options.CustomFilter == null || options.CustomFilter(episode));

    private static EpisodeDataPoint CreateDataPoint(Episode episode) =>
        new()
        {
            Id = episode.Id,
            EraDescription = episode.EraDescription,
            Doctor = episode.Doctor,
            Actor = episode.Actor,
            Season = episode.Season,
            SeasonFormatDescription = episode.SeasonFormatDescription,
            SeasonDescription = episode.SeasonDescription,
            Story = episode.Story,
            StoryInSeason = episode.StoryInSeason,
            StoryTitle = episode.StoryTitle,
            PartInStory = episode.PartInStory,
            PartTitle = episode.PartTitle,
            EpisodeInSeason = episode.EpisodeInSeason,
            FullTitle = episode.FullTitle,
            Slug = episode.Slug,
            OriginalAirDate = episode.OriginalAirDate,
            Runtime = episode.Runtime,
            IsMissing = episode.IsMissing,
            Writers = episode.Writers,
            WriterAlias = episode.WriterAlias,
            OvernightRatings = episode.OvernightRatings,
            ConsolidatedRatings = episode.ConsolidatedRatings,
            ConsolidatedExcessRatings = episode.ConsolidatedExcessRatings,
            ExtendedRatings = episode.ExtendedRatings,
            ExtendedExcessRatings = episode.ExtendedExcessRatings,
            PopulationAdjustedOvernightRatings = episode.PopulationAdjustedOvernightRatings,
            PopulationAdjustedConsolidatedRatings = episode.PopulationAdjustedConsolidatedRatings,
            PopulationAdjustedExtendedRatings = episode.PopulationAdjustedExtendedRatings,
            PopulationAdjustedConsolidatedExcessRatings = episode.PopulationAdjustedConsolidatedExcessRatings,
            PopulationAdjustedExtendedExcessRatings = episode.PopulationAdjustedExtendedExcessRatings,
            Population = episode.Population,
            OvernightPercentageOfPopulation = episode.OvernightPercentageOfPopulation,
            ConsolidatedPercentageOfPopulation = episode.ConsolidatedPercentageOfPopulation,
            ExtendedPercentageOfPopulation = episode.ExtendedPercentageOfPopulation,
            Note = episode.Note,
            WikiUrl = episode.WikiUrl
        };
}
