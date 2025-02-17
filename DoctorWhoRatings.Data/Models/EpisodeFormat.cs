namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the format of a Doctor Who episode, e.g. a regular episode, special, or movie.
/// <para>
/// See also EpisodeFormats enum.
/// </para>
/// </summary>
public class EpisodeFormat
{
    public int Id { get; init; }

    public required string Description { get; init; }
}