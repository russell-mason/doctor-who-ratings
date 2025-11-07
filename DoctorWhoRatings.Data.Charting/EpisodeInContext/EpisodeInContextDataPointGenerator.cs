namespace DoctorWhoRatings.Data.Charting.EpisodeInContext;

public class EpisodeInContextDataPointGenerator(IEpisodesDataPointGenerator dataPointGenerator) : IEpisodeInContextDataPointGenerator
{
    public EpisodeInContextDataPoint Generate(EpisodeInContextDataOptions options)
    {
        var episode = dataPointGenerator.Filter(new EpisodesDataOptions { SlugFilter = options.Slug }).FirstOrDefault()
                      ?? throw new KeyNotFoundException();

        var storyEpisodes = dataPointGenerator.Filter(new EpisodesDataOptions { DoctorFilter = episode.Doctor, StoryFilter = episode.Story });

        var seasonEpisodes = episode.EpisodeFormatId == EpisodeFormats.Special
            ? dataPointGenerator.Filter(new EpisodesDataOptions { DoctorFilter = episode.Doctor, EpisodeFormatIdFilter = episode.EpisodeFormatId })
            : dataPointGenerator.Filter(new EpisodesDataOptions { EraId = episode.EraId, SeasonFilter = episode.Season });

        var doctorEpisodes = dataPointGenerator.Filter(new EpisodesDataOptions { DoctorFilter = episode.Doctor });
        var allEpisodes = dataPointGenerator.Filter(new EpisodesDataOptions());

        var dataPoint = new EpisodeInContextDataPoint
        {
            Episode = dataPointGenerator.Generate(episode),
            InStoryContext = CreateContext(storyEpisodes, episode, false),
            InSeasonContext = CreateContext(seasonEpisodes, episode, true),
            InDoctorContext = CreateContext(doctorEpisodes, episode, true),
            InFullContext = CreateContext(allEpisodes, episode, true)!
        };

        return dataPoint;
    }

    private EpisodeContext? CreateContext(List<Episode> contextEpisodes, Episode sourceEpisode, bool minMaxSample)
    {
        if (contextEpisodes.Count == 1)
        {
            return null;
        }

        var orderedByHighestRatings = contextEpisodes.OrderByDescending(episode => episode.OvernightRatings)
                                                     .ThenBy(episode => episode.Id)
                                                     .ToList();

        var positionFromTop = orderedByHighestRatings.IndexOf(sourceEpisode);
        var positionFromBottom = contextEpisodes.Count - positionFromTop - 1;

        var relativeToHighestRatings = orderedByHighestRatings.First().OvernightRatings - sourceEpisode.OvernightRatings;
        var relativeToAverageRatings = sourceEpisode.OvernightRatings - contextEpisodes.Average(episode => episode.OvernightRatings);
        var relativeToLowestRatings = orderedByHighestRatings.Last().OvernightRatings - sourceEpisode.OvernightRatings;

        var sampledEpisodes = CreateSamples(contextEpisodes, sourceEpisode, minMaxSample);

        return new EpisodeContext
        {
            IsSpecial = sourceEpisode.EpisodeFormatId == EpisodeFormats.Special,
            PositionFromTop = positionFromTop,
            PositionFromBottom = positionFromBottom,
            EpisodeCount = contextEpisodes.Count,
            MissingEpisodeCount = contextEpisodes.Count(episode => episode.IsMissing),
            StoryCount = contextEpisodes.Max(episode => episode.Story) - contextEpisodes.Min(episode => episode.Story) + 1,
            RelativeToHighestRatings = relativeToHighestRatings,
            RelativeToAverageRatings = relativeToAverageRatings,
            RelativeToLowestRatings = relativeToLowestRatings,
            EpisodeIndex = sampledEpisodes.IndexOf(sourceEpisode),
            Episodes = sampledEpisodes.Select(dataPointGenerator.Generate).ToList()
        };
    }

    private static List<Episode> CreateSamples(List<Episode> contextEpisodes, Episode sourceEpisode, bool minMaxSample)
    {
        if (!minMaxSample)
        {
            return contextEpisodes;
        }

        var orderedEpisodes = contextEpisodes.OrderByDescending(episode => episode.OvernightRatings)
                                             .ThenBy(episode => episode.Id)
                                             .ToList();

        var sampledEpisodes = new List<Episode> { orderedEpisodes.First(), orderedEpisodes.Last() };

        if (!sampledEpisodes.Contains(sourceEpisode))
        {
            sampledEpisodes.Insert(1, sourceEpisode);
        }

        return sampledEpisodes;
    }
}