namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IEpisodeGuideDataPointGenerator
{
    public EpisodeGuideDataPoint Generate(EpisodeGuideDataOptions options)
    {
        var doctorGroups = dataProvider.DoctorWhoData.Episodes.GroupBy(episode => new DoctorInfo(episode));

        var doctorDataPoints = doctorGroups
                               .Select(group => CreateDoctorDataPoint(group, options))
                               .Where(doctor => doctor.Seasons.Count > 0)
                               .ToList();

        var episodeGuideDataPoint = new EpisodeGuideDataPoint
        {
            Doctors = doctorDataPoints
        };

        return episodeGuideDataPoint;
    }

    private EpisodeGuideDoctorDataPoint CreateDoctorDataPoint(IGrouping<DoctorInfo, Episode> doctorEpisodes,
                                                              EpisodeGuideDataOptions options)
    {
        var seasonGroups = doctorEpisodes.GroupEpisodesBy(episode => new SeasonInfo(episode));

        var seasonDataPoints = CreateSeasonDataPoints(seasonGroups, options)
                               .Where(season => season.Stories.Count > 0)
                               .ToList();

        var hasMissingEpisodes = seasonDataPoints
            .Any(season => season.Stories
                                 .Any(story => story.Episodes.Any(episode => episode.IsMissing)));

        var doctorDataPoint = new EpisodeGuideDoctorDataPoint
        {
            Actor = doctorEpisodes.Key.Actor,
            Doctor = doctorEpisodes.Key.Doctor,
            Seasons = seasonDataPoints,
            HasMissingEpisodes = hasMissingEpisodes
        };

        return doctorDataPoint;
    }

    private List<EpisodeGuideSeasonDataPoint> CreateSeasonDataPoints(List<IGrouping<SeasonInfo, Episode>> seasonGroups,
                                                                     EpisodeGuideDataOptions options)
    {
        var seasonDataPoints = seasonGroups
                               .Select(seasonGroup =>
                               {
                                   // Need to include the story tile for Trial of a Time Lord which has multiple sub-stories in the same story
                                   var storyGroups =
                                       seasonGroup.GroupEpisodesBy(episode => (episode.Story, episode.StoryTitle), episode => new StoryInfo(episode));

                                   var storyDataPoints = CreateStoryDataPoints(storyGroups, options);

                                   var seasonDataPoint = new EpisodeGuideSeasonDataPoint
                                   {
                                       Season = seasonGroup.Key.Season,
                                       SeasonFormatId = seasonGroup.Key.SeasonFormatId,
                                       SeasonFormatDescription = seasonGroup.Key.SeasonFormatDescription,
                                       Stories = storyDataPoints
                                   };

                                   return seasonDataPoint;
                               })
                               .Where(season => season.Stories.Count > 0)
                               .ToList();

        return seasonDataPoints;
    }

    private List<EpisodeGuideStoryDataPoint> CreateStoryDataPoints(List<IGrouping<StoryInfo, Episode>> storyGroups,
                                                                   EpisodeGuideDataOptions options)
    {
        var storyDataPoints = storyGroups
                              .Select(storyGroup =>
                              {
                                  var episodeDataPoints = storyGroup
                                                          .Where(episode => EpisodeMatchesFilters(episode, options))
                                                          .Select(episode => CreateEpisodeDataPoint(episode, options))
                                                          .ToList();

                                  var seasonDataPoint = new EpisodeGuideStoryDataPoint
                                  {
                                      Story = storyGroup.Key.Story,
                                      StoryTitle = storyGroup.Key.StoryTitle,
                                      StoryInSeason = storyGroup.Key.StoryInSeason,
                                      WikiUrl = storyGroup.Key.StoryWikiUrl,
                                      Episodes = episodeDataPoints
                                  };

                                  return seasonDataPoint;
                              })
                              .Where(story => StoryMatchesFilters(story, options))
                              .ToList();

        return storyDataPoints;
    }

    private static bool StoryMatchesFilters(EpisodeGuideStoryDataPoint story, EpisodeGuideDataOptions options) =>
        (options.MissingEpisodeHandling != MissingEpisodeHandling.RestrictTo &&
         (story.StoryTitle ?? "").Contains(options.Filter, StringComparison.OrdinalIgnoreCase)) ||
         story.Episodes.Count > 0;

    private static bool EpisodeMatchesFilters(Episode episode, EpisodeGuideDataOptions options) =>
        (episode.PartTitle ?? episode.StoryTitle).Contains(options.Filter, StringComparison.OrdinalIgnoreCase) &&
        options.MissingEpisodeHandling switch
        {
            MissingEpisodeHandling.Include => true,
            MissingEpisodeHandling.Exclude => !episode.IsMissing,
            MissingEpisodeHandling.RestrictTo => episode.IsMissing,
            _ => false
        };

    private EpisodeGuideEpisodeDataPoint CreateEpisodeDataPoint(Episode episode, EpisodeGuideDataOptions options)
    {
        var episodeDataPoint = new EpisodeGuideEpisodeDataPoint
        {
            EpisodeFormatId = episode.EpisodeFormatId,
            EpisodeNumber = episode.Id,
            PartInStory = episode.PartInStory,
            PartTitle = episode.PartTitle ?? episode.StoryTitle,
            IsMissing = episode.IsMissing,
            OriginalAirDate = episode.OriginalAirDate,
            OvernightRatings = episode.OvernightRatings,
            ConsolidatedRatings = episode.ConsolidatedRatings,
            ExtendedRatings = episode.ExtendedRatings,
            WikiUrl = episode.WikiFormatId == WikiFormats.Episode ? episode.WikiUrl : null,
            Slug = episode.Slug
        };

        return episodeDataPoint;
    }

    private record DoctorInfo(int Doctor, string Actor)
    {
        public DoctorInfo(Episode episode) : this(episode.Doctor, episode.Actor) { }
    }

    private record SeasonInfo(int? Season, int SeasonFormatId, string SeasonFormatDescription)
    {
        public SeasonInfo(Episode episode) : this(episode.Season, episode.SeasonFormatId, episode.SeasonFormatDescription) { }
    }

    private record StoryInfo(int Story, int? StoryInSeason, string? StoryTitle, string? StoryWikiUrl)
    {
        public StoryInfo(Episode episode) :
            this(episode.Story,
                 episode.StoryInSeason,
                 string.IsNullOrWhiteSpace(episode.PartTitle) ? null : episode.StoryTitle,
                 episode.WikiFormatId == WikiFormats.Story ? episode.WikiUrl : null)
        { }
    }
}