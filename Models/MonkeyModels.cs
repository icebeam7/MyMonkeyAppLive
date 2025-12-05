namespace MyMonkeyApp.Models;

/// <summary>
/// Represents geographical coordinates with latitude and longitude
/// </summary>
public class Location
{
    /// <summary>
    /// Gets or sets the latitude coordinate
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Returns a string representation of the location
    /// </summary>
    /// <returns>Formatted latitude and longitude string</returns>
    public override string ToString()
    {
        return $"{Latitude:F6}, {Longitude:F6}";
    }
}

/// <summary>
/// Represents a point along a monkey's journey path
/// </summary>
public class PathPoint
{
    /// <summary>
    /// Gets or sets the location at this path point
    /// </summary>
    public Location Location { get; set; } = new();

    /// <summary>
    /// Gets or sets the timestamp when the monkey was at this location
    /// </summary>
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Represents an activity performed by a monkey during its journey
/// </summary>
public class MonkeyActivity
{
    /// <summary>
    /// Gets or sets the type of activity (e.g., "Foraging", "Grooming")
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the activity
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location where the activity took place
    /// </summary>
    public Location Location { get; set; } = new();

    /// <summary>
    /// Gets or sets the timestamp when the activity occurred
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the duration of the activity
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets or sets the energy change from this activity (positive = gained, negative = lost)
    /// </summary>
    public int EnergyChange { get; set; }

    /// <summary>
    /// Returns a string representation of the activity
    /// </summary>
    /// <returns>Formatted activity information</returns>
    public override string ToString()
    {
        var energyText = EnergyChange >= 0 ? $"+{EnergyChange}" : EnergyChange.ToString();
        return $"{Type}: {Description} (Energy: {energyText})";
    }
}

/// <summary>
/// Represents the health statistics of a monkey
/// </summary>
public class HealthStats
{
    /// <summary>
    /// Gets or sets the energy level (0-100)
    /// </summary>
    public int Energy { get; set; }

    /// <summary>
    /// Gets or sets the happiness level (0-100)
    /// </summary>
    public int Happiness { get; set; }

    /// <summary>
    /// Gets or sets the hunger level (0-100, higher = more hungry)
    /// </summary>
    public int Hunger { get; set; }

    /// <summary>
    /// Gets or sets the social interaction level (0-100)
    /// </summary>
    public int Social { get; set; }

    /// <summary>
    /// Gets or sets the stress level (0-100, higher = more stressed)
    /// </summary>
    public int Stress { get; set; }

    /// <summary>
    /// Gets or sets the overall health level (0-100)
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Gets the overall wellbeing score based on all health stats
    /// </summary>
    /// <returns>A calculated wellbeing score (0-100)</returns>
    public double GetWellbeingScore()
    {
        // Calculate wellbeing as average of positive stats minus negative stats
        var positiveStats = (Energy + Happiness + Social + Health) / 4.0;
        var negativeStats = (Hunger + Stress) / 2.0;
        return Math.Max(0, Math.Min(100, positiveStats - (negativeStats * 0.3)));
    }

    /// <summary>
    /// Returns a string representation of the health stats
    /// </summary>
    /// <returns>Formatted health information</returns>
    public override string ToString()
    {
        return $"Health: {Health} | Energy: {Energy} | Happiness: {Happiness} | Stress: {Stress}";
    }
}