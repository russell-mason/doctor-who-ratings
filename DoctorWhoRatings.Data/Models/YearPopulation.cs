namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Maps a year to the UK population at that time.
/// </summary>
public record YearPopulation
{
    public int Year { get; init; }

    public int Population { get; init; }
    
    public string? Note { get; init; }
}