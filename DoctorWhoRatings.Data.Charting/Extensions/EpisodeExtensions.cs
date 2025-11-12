namespace DoctorWhoRatings.Data.Charting.Extensions;

public static class EpisodeExtensions
{
    extension(Episode referenceEpisode)
    {
        public string DisambiguateActor(IReadOnlyList<Doctor> doctors) =>
            doctors.Count(doctor => doctor.Actor == referenceEpisode.Actor) > 1
                ? $"{referenceEpisode.Actor} ({referenceEpisode.Doctor})"
                : referenceEpisode.Actor;
    }

    extension(IEnumerable<Episode> episodes)
    {
        public List<IGrouping<TKey, Episode>> GroupEpisodesBy<TKey>(Func<Episode, TKey> keySelector) =>
            episodes.GroupEpisodesBy(keySelector, keySelector);

        public List<IGrouping<TKey, Episode>> GroupEpisodesBy<TGroup, TKey>(Func<Episode, TGroup> groupSelector,
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
    }

    private class Grouping<TKey, TElement>(TKey key, IEnumerable<TElement> elements) : IGrouping<TKey, TElement>
    {
        public TKey Key { get; } = key;

        public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
