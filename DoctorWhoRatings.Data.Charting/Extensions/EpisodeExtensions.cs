namespace DoctorWhoRatings.Data.Charting.Extensions;

public static class EpisodeExtensions
{
    public static string DisambiguateActor(this Episode referenceEpisode, IReadOnlyList<Doctor> doctors)
    {
        var isRepeatingActor = doctors.Count(doctor => doctor.Actor == referenceEpisode.Actor) > 1;

        return isRepeatingActor ? $"{referenceEpisode.Actor} ({referenceEpisode.Doctor})" : referenceEpisode.Actor;
    }

    public static List<IGrouping<TKey, Episode>> GroupEpisodesBy<TKey>(this IEnumerable<Episode> episodes,
                                                                       Func<Episode, TKey> keySelector) =>
        episodes.GroupEpisodesBy(keySelector, keySelector);


    public static List<IGrouping<TKey, Episode>> GroupEpisodesBy<TGroup, TKey>(this IEnumerable<Episode> episodes,
                                                                               Func<Episode, TGroup> groupSelector,
                                                                               Func<Episode, TKey> keySelector)
    {
        List<IGrouping<TKey, Episode>> groups = [];
        TGroup? lastGroupValue = default;
        TKey? lastKey = default;
        List<Episode> currentGroupEpisodes = [];

        foreach (var episode in episodes)
        {
            var groupValue = groupSelector(episode);
            var key = keySelector(episode);

            if (!EqualityComparer<TGroup>.Default.Equals(groupValue, lastGroupValue))
            {
                if (currentGroupEpisodes.Count > 0)
                {
                    groups.Add(new Grouping<TKey, Episode>(lastKey!, currentGroupEpisodes));
                    currentGroupEpisodes = [];
                }

                lastGroupValue = groupValue;
                lastKey = key;
            }

            currentGroupEpisodes.Add(episode);
        }

        if (currentGroupEpisodes.Count > 0)
        {
            groups.Add(new Grouping<TKey, Episode>(lastKey!, currentGroupEpisodes));
        }

        return groups;
    }

    private class Grouping<TKey, TElement>(TKey key, IEnumerable<TElement> elements) : IGrouping<TKey, TElement>
    {
        public TKey Key { get; } = key;

        public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
