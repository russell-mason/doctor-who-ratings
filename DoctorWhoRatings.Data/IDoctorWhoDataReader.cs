namespace DoctorWhoRatings.Data;

/// <summary>
/// Represents the means by which data is read from a file.
/// </summary>
public interface IDoctorWhoDataReader
{
    /// <summary>
    /// Reads all data from the file located at the specified path.
    /// </summary>
    /// <param name="path">The path to the data file.</param>
    /// <returns>The root dataset.</returns>
    DoctorWhoData Read(string path);
}