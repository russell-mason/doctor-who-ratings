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

    /// <summary>
    /// Gets a collection of all doctors.
    /// </summary>
    public IReadOnlyList<Doctor> Doctors { get; init; } = [];

    /// <summary>
    /// Gets a collection of all season formats.
    /// </summary>
    public IReadOnlyList<SeasonFormat> SeasonFormats { get; init; } = [];

    /// <summary>
    /// Gets a collection of all episode formats.
    /// </summary>
    public IReadOnlyList<EpisodeFormat> EpisodeFormats { get; init; } = [];

    /// <summary>
    /// Gets a collection of all eras.
    /// </summary>
    public IReadOnlyList<Era> Eras { get; init; } = [];

    /// <summary>
    /// Gets a collection of all wiki formats.
    /// </summary>
    public IReadOnlyList<WikiFormat> WikiFormats { get; init; } = [];

    /// <summary>
    /// Gets a collection of population details by year.
    /// </summary>
    public IReadOnlyList<YearPopulation> Populations { get; init; } = [];
}