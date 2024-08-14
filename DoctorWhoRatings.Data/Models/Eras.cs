namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the era, Classic, New, etc., in which an episode of Doctor Who appears in.
/// <para>This matches Episode.EraId allowing for filtering.</para>
/// </summary>
public static class Eras
{
    public const int Classic = 1;

    public const int Universal = 2;

    public const int New = 3;

    public const int Disney = 4;
}
