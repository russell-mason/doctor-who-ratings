namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the format of a Wiki URL, e.g. a story, or an individual episode.
/// <para>
/// See also WikiFormats enum.
/// </para>
/// </summary>
public class WikiFormat
{
    public int Id { get; init; }

    public required string Description { get; init; }
}