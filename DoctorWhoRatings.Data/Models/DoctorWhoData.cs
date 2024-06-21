namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Master dataset providing a single source of truth for all queries.
/// </summary>
public record DoctorWhoData
{
    /// <summary>
    /// Gets a collection of all episodes, specials, and movies.
    /// </summary>
    public IReadOnlyList<Episode> Episodes { get; init; } = [];
}
