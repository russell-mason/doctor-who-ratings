namespace DoctorWhoRatings.Data.Charting.Dashboard;

public class DashboardData
{
    public required (int years, int months, int days) PeriodSinceFirstEpisode { get; init; }

    public required (int years, int months, int days) PeriodSinceLastEpisode { get; init; }

    public int NumberOfDoctors { get; init; }

    public int NumberOfSeasons { get; init; }

    public int TotalNumberOfEpisodes { get; init; }

    public int NumberOfStories { get; init; }

    public int NumberOfSeriesEpisodes { get; init; }

    public int NumberOfMissingEpisodes { get; init; }

    public int NumberOfSpecials { get; init; }

    public int NumberOfMovies { get; init; }

    public decimal TotalEpisodeHours { get; init; }

    public decimal TotalHoursWatched { get; init; }

    public decimal TotalRatings { get; init; }
}
