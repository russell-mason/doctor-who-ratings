namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the format of a Doctor Who season, e.g. Classic Who uses the term season, New Who uses the term series.
/// <para>
/// See also SeasonFormats enum.
/// </para>
/// </summary>
public class SeasonFormat
{
    public int Id { get; init; }

    public required string Description { get; init; }
}