namespace MyMonkeyApp.Models;

/// <summary>
/// Represents a monkey species with its basic information and characteristics
/// </summary>
public class Monkey
{
    /// <summary>
    /// Gets or sets the name of the monkey species
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographical location where the monkey is found
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets detailed information about the monkey species
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the monkey's image
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the population count of the monkey species
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the monkey's habitat
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the monkey's habitat
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Returns a string representation of the monkey
    /// </summary>
    /// <returns>Formatted string with monkey details</returns>
    public override string ToString()
    {
        return $"{Name} - {Location} (Population: {Population:N0})";
    }

    /// <summary>
    /// Gets the coordinates as a formatted string
    /// </summary>
    /// <returns>Latitude and longitude formatted as a string</returns>
    public string GetCoordinates()
    {
        return $"{Latitude:F6}, {Longitude:F6}";
    }

    /// <summary>
    /// Determines if this monkey species is endangered (population < 5000)
    /// </summary>
    /// <returns>True if endangered, false otherwise</returns>
    public bool IsEndangered()
    {
        return Population < 5000;
    }
}