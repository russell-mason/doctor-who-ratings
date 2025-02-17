namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents the era, e.g. Classic Who, New Who, etc.
/// <para>
/// See also Eras enum.
/// </para>
/// </summary>
public class Era
{
    public int Id { get; init; }

    public required string Description { get; init; }
}