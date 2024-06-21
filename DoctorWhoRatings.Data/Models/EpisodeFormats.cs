namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the format of a Doctor Who episode.
/// <para>This matches Episode.EpisodeFormatId allowing for filtering.</para>
/// </summary>
public static class EpisodeFormats
{
    public const int Series = 1;

    public const int Special = 2;

    public const int Movie = 3;
}
