﻿namespace DoctorWhoRatings.Data.Charting.ShareByHoursWatched;

/// <summary>
/// Generates a chart-based data source specifically designed to show the total number of hours watched for each doctor.
/// </summary>
public interface IShareByHoursWatchedDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the total hours are calculated.</param>
    /// <returns>A specific structure containing hours watched data.</returns>
    List<ShareByHoursWatchedDataPoint> Generate(ShareByHoursWatchedDataOptions options);
}