namespace DoctorWhoRatings.Data;

/// <summary>
/// Represents a provider for all access to the root dataset. Abstracts the actual data retrieval mechanism.
/// </summary>
public interface IDoctorWhoDataProvider
{
    /// <summary>
    /// Gets the root dataset.
    /// </summary>
    DoctorWhoData DoctorWhoData { get; }
}
