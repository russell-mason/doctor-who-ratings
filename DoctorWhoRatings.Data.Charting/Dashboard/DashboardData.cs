namespace DoctorWhoRatings.Data.Charting.Dashboard;

public class DashboardData
{
    public required (int years, int months, int days) PeriodSinceFirstEpisode { get; set; }

    public required (int years, int months, int days) PeriodSinceLastEpisode { get; set; }

    public int NumberOfDoctors { get; set; }

    public int NumberOfSeasons { get; set; }

    public int TotalNumberOfEpisodes { get; set; }

    public int NumberOfStories { get; set; }

    public int NumberOfSeriesEpisodes { get; set; }

    public int NumberOfSpecials { get; set; }

    public int NumberOfMovies { get; set; }

    public decimal TotalEpisodeHours { get; set; }

    public decimal TotalHoursWatched { get; set; }

    public decimal TotalRatings { get; set; }
}
