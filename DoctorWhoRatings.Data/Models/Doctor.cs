namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represent the Actor who plays the main role, and which incarnation they are.
/// </summary>
public class Doctor
{
    public int Number { get; init; }

    public required string Actor { get; init; }
}
