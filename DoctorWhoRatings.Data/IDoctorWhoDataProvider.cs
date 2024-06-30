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

    /// <summary>
    /// Loads the root dataset. Can be used for preloading, otherwise the dataset will be loaded when the
    /// DoctorWhoData property is accessed.
    /// </summary>
    void Load();
}
