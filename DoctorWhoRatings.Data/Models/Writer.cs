namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents an episode writer.
/// </summary>
public class Writer
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public bool IsAlias { get; init; }

    public string? Note { get; init; }
}