﻿namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodesDataOptions
{
    public int? DoctorFilter { get; set; }

    public bool AdjustForCurrentPopulation { get; set; } = false;
}