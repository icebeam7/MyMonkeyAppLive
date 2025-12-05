namespace MyMonkeyApp.Models;

/// <summary>
/// Represents a complete journey undertaken by a monkey, including path, activities, and health stats
/// </summary>
public class MonkeyJourney
{
    /// <summary>
    /// Gets or sets the name of the monkey that took this journey
    /// </summary>
    public string MonkeyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the starting location of the journey
    /// </summary>
    public Location StartLocation { get; set; } = new();

    /// <summary>
    /// Gets or sets the start time of the journey
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the journey
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Gets or sets the list of path points along the journey
    /// </summary>
    public List<PathPoint> PathPoints { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of activities performed during the journey
    /// </summary>
    public List<MonkeyActivity> Activities { get; set; } = new();

    /// <summary>
    /// Gets or sets the health statistics at the end of the journey
    /// </summary>
    public HealthStats HealthStats { get; set; } = new();

    /// <summary>
    /// Gets or sets the total duration of the journey
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    /// Gets or sets the total distance traveled in kilometers
    /// </summary>
    public double TotalDistanceKm { get; set; }

    /// <summary>
    /// Gets the number of activities performed during the journey
    /// </summary>
    public int ActivityCount => Activities.Count;

    /// <summary>
    /// Gets the total energy change from all activities
    /// </summary>
    public int TotalEnergyChange => Activities.Sum(a => a.EnergyChange);

    /// <summary>
    /// Gets the most common activity type during the journey
    /// </summary>
    public string? MostCommonActivity => Activities
        .GroupBy(a => a.Type)
        .OrderByDescending(g => g.Count())
        .FirstOrDefault()?.Key;

    /// <summary>
    /// Gets the average speed of the journey in km/h
    /// </summary>
    public double AverageSpeedKmh
    {
        get
        {
            if (TotalDuration.TotalHours == 0) return 0;
            return TotalDistanceKm / TotalDuration.TotalHours;
        }
    }

    /// <summary>
    /// Returns a summary string of the journey
    /// </summary>
    /// <returns>Formatted journey summary</returns>
    public override string ToString()
    {
        return $"{MonkeyName}'s Journey: {TotalDistanceKm:F2}km in {TotalDuration:hh\\:mm\\:ss} " +
               $"with {ActivityCount} activities";
    }

    /// <summary>
    /// Gets activities of a specific type
    /// </summary>
    /// <param name="activityType">The type of activity to filter by</param>
    /// <returns>List of activities of the specified type</returns>
    public List<MonkeyActivity> GetActivitiesByType(string activityType)
    {
        return Activities.Where(a => a.Type.Equals(activityType, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Gets the journey statistics summary
    /// </summary>
    /// <returns>Dictionary containing various journey statistics</returns>
    public Dictionary<string, object> GetJourneyStats()
    {
        return new Dictionary<string, object>
        {
            ["Distance"] = $"{TotalDistanceKm:F2} km",
            ["Duration"] = TotalDuration.ToString(@"hh\:mm\:ss"),
            ["Average Speed"] = $"{AverageSpeedKmh:F1} km/h",
            ["Activities"] = ActivityCount,
            ["Energy Change"] = TotalEnergyChange,
            ["Most Common Activity"] = MostCommonActivity ?? "None",
            ["Wellbeing Score"] = $"{HealthStats.GetWellbeingScore():F1}/100"
        };
    }
}