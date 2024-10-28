namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IEpisodeGuideDataPointGenerator
{
    public List<EpisodeGuideDoctorDataPoint> Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => (episode.Doctor, episode.Actor))
                                     .Select(CreateDoctorDataPoint)
                                     .ToList();

        return dataPoints;
    }

    private static EpisodeGuideDoctorDataPoint CreateDoctorDataPoint(IGrouping<(int doctor, string actor), Episode> group)
    {
        var doctorDataPoint = new EpisodeGuideDoctorDataPoint
        {
            Doctor = group.Key.doctor,
            Actor = group.Key.actor,
            Episodes = CreateEpisodeGuideDataPoints(group.ToList())
        };

        return doctorDataPoint;
    }

    private static List<EpisodeGuideDataPoint> CreateEpisodeGuideDataPoints(List<Episode> episodes) =>
        episodes.Select((_, index) => CreateEpisodeGuideDataPoint(episodes, index)).ToList();

    private static EpisodeGuideDataPoint CreateEpisodeGuideDataPoint(List<Episode> episodes, int index)
    {
        var currentEpisode = episodes[index];

        var episodeDataPoint = new EpisodeGuideDataPoint
        {
            EpisodeFormatId = currentEpisode.EpisodeFormatId,
            Season = currentEpisode.Season,
            SeasonFormatDescription = currentEpisode.SeasonFormatDescription,
            EpisodeNumber = currentEpisode.Id,
            Story = currentEpisode.Story,
            StoryTitle = currentEpisode.StoryTitle,
            PartInStory = currentEpisode.PartInStory,
            PartTitle = currentEpisode.PartTitle,
            OriginalAirDate = currentEpisode.OriginalAirDate,
            OvernightRatings = currentEpisode.OvernightRatings,
            ConsolidatedRatings = currentEpisode.ConsolidatedRatings,
            ExtendedRatings = currentEpisode.ExtendedRatings,
            WikiUrl = currentEpisode.WikiUrl,
            Metadata = CreateEpisodeGuideDataPointMetadata(episodes, index)
        };

        return episodeDataPoint;
    }

    private static EpisodeGuideDataPointMetadata CreateEpisodeGuideDataPointMetadata(List<Episode> episodes, int index)
    {
        var previousEpisode = index > 0 ? episodes[index - 1] : null;
        var currentEpisode = episodes[index];
        var nextEpisode = index < episodes.Count - 1 ? episodes[index + 1] : null;

        var showSeasonTitle = (currentEpisode.Season != null &&
                               currentEpisode.Season != previousEpisode?.Season) ||
                              (currentEpisode.EpisodeFormatId == EpisodeFormats.Movie) ||
                              (currentEpisode.EpisodeFormatId == EpisodeFormats.Special &&
                               previousEpisode?.EpisodeFormatId != EpisodeFormats.Special);

        string? seasonTitle = null;

        if (showSeasonTitle)
        {
            var plural = previousEpisode?.EpisodeFormatId != EpisodeFormats.Special &&
                         currentEpisode.EpisodeFormatId == EpisodeFormats.Special &&
                         nextEpisode is { EpisodeFormatId: EpisodeFormats.Special }
                ? "s"
                : "";

            seasonTitle = $"{currentEpisode.SeasonFormatDescription}{plural} {currentEpisode.Season}";
        }

        var showStoryTitle = (currentEpisode.PartInStory == 1 || currentEpisode.StoryTitle != previousEpisode?.StoryTitle) && 
                             nextEpisode?.PartInStory > 1 &&
                             !string.IsNullOrWhiteSpace(currentEpisode.PartTitle);

        var showStoryNumber = currentEpisode.PartTitle == null && currentEpisode.PartInStory == 1;

        var hasStoryWiki = showStoryTitle && !string.IsNullOrWhiteSpace(currentEpisode.WikiUrl);

        var hasEpisodeWiki = currentEpisode.PartTitle == null && 
                             !showStoryTitle && !string.IsNullOrWhiteSpace(currentEpisode.WikiUrl);

        var episodeIndentIndicator = currentEpisode.PartTitle != null ? "indent" : string.Empty;

        var consecutiveSeasonStoryIndicator = showSeasonTitle && showStoryTitle ? "first" : string.Empty;

        var metaData = new EpisodeGuideDataPointMetadata
        {
            ShowSeasonTitle = showSeasonTitle,
            SeasonTitle = seasonTitle,
            ShowStoryTitle = showStoryTitle,
            ShowStoryNumber = showStoryNumber,
            HasStoryWiki = hasStoryWiki,
            EpisodeTitle = currentEpisode.PartTitle ?? currentEpisode.StoryTitle,
            HasEpisodeWiki = hasEpisodeWiki,
            EpisodeIndentIndicator = episodeIndentIndicator,
            ConsecutiveSeasonStoryIndicator = consecutiveSeasonStoryIndicator
        };

        return metaData;
    }
}
