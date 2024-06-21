namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the format of a Doctor Who season.
/// <para>Specials and movies, are not part of a season.</para>
/// <para>This matches Episode.SeasonFormatId allowing for filtering.</para>
/// </summary>
public static class SeasonFormats
{
    public const int Season = 1;

    public const int Movie = 2;

    public const int Series = 3;

    public const int Special = 4;
}
