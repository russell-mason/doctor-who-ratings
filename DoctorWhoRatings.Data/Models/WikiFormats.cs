namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Indicates whether the wiki URL relates to the overarching story, or the individual episode.
/// <para>This matches Episode.WikiFormatId allowing for simpler association.</para>
/// </summary>
public static class WikiFormats
{
    public const int Story = 1;

    public const int Episode = 2;
}

