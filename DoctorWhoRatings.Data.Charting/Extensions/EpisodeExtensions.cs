namespace DoctorWhoRatings.Data.Charting.Extensions;

public static class EpisodeExtensions
{
    public static string DisambiguateActor(this Episode referenceEpisode, IReadOnlyList<Doctor> doctors)
    {
        var isRepeatingActor = doctors.Count(doctor => doctor.Actor == referenceEpisode.Actor) > 1;

        return isRepeatingActor ? $"{referenceEpisode.Actor} ({referenceEpisode.Doctor})" : referenceEpisode.Actor;
    }
}
