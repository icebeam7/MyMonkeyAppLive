using MyMonkeyApp.Models;
using System.Text.Json;

namespace MyMonkeyApp.Helpers;

/// <summary>
/// Helper class for managing monkey data and interactions with MonkeyMCP server
/// </summary>
public static class MonkeyHelper
{
    private static readonly Random random = new();
    private static List<Monkey>? cachedMonkeys;
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Gets all available monkeys from the MCP server
    /// </summary>
    /// <returns>List of all monkeys</returns>
    public static async Task<List<Monkey>> GetAllMonkeysAsync()
    {
        if (cachedMonkeys != null)
        {
            return cachedMonkeys;
        }

        try
        {
            // For now, return a sample list since we can't directly call MCP from here
            // In a real implementation, you would make HTTP calls to the MCP server
            cachedMonkeys = GetSampleMonkeys();
            return cachedMonkeys;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting monkeys: {ex.Message}");
            return new List<Monkey>();
        }
    }

    /// <summary>
    /// Gets a random monkey from the collection
    /// </summary>
    /// <returns>A randomly selected monkey or null if none available</returns>
    public static async Task<Monkey?> GetRandomMonkeyAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        if (monkeys.Count == 0)
        {
            return null;
        }

        var randomIndex = random.Next(monkeys.Count);
        return monkeys[randomIndex];
    }

    /// <summary>
    /// Gets multiple random monkeys from the collection
    /// </summary>
    /// <param name="count">Number of monkeys to return</param>
    /// <returns>List of randomly selected monkeys</returns>
    public static async Task<List<Monkey>> GetMultipleRandomMonkeysAsync(int count)
    {
        var allMonkeys = await GetAllMonkeysAsync();
        if (allMonkeys.Count == 0)
        {
            return new List<Monkey>();
        }

        var selectedMonkeys = new List<Monkey>();
        var availableMonkeys = new List<Monkey>(allMonkeys);

        for (int i = 0; i < count && availableMonkeys.Count > 0; i++)
        {
            var randomIndex = random.Next(availableMonkeys.Count);
            selectedMonkeys.Add(availableMonkeys[randomIndex]);
            availableMonkeys.RemoveAt(randomIndex); // Avoid duplicates
        }

        return selectedMonkeys;
    }

    /// <summary>
    /// Gets a specific monkey by name
    /// </summary>
    /// <param name="name">The name of the monkey to find</param>
    /// <returns>The monkey with the specified name or null if not found</returns>
    public static async Task<Monkey?> GetMonkeyByNameAsync(string name)
    {
        var monkeys = await GetAllMonkeysAsync();
        return monkeys.FirstOrDefault(m => 
            m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets journey information for a specific monkey
    /// </summary>
    /// <param name="monkeyName">The name of the monkey</param>
    /// <returns>The monkey's journey data or null if not available</returns>
    public static async Task<MonkeyJourney?> GetMonkeyJourneyAsync(string monkeyName)
    {
        try
        {
            // For now, return a sample journey
            // In a real implementation, you would call the MCP server
            return GetSampleJourney(monkeyName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting journey for {monkeyName}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets monkey business emojis
    /// </summary>
    /// <returns>Random monkey-related emojis</returns>
    public static async Task<string> GetMonkeyBusinessAsync()
    {
        try
        {
            // For now, return sample monkey business
            // In a real implementation, you would call the MCP server
            var monkeyEmojis = new[] { "🐒", "🐵", "🙈", "🙉", "🙊", "🍌", "🌴", "🥥", "🌺", "🦧" };
            var count = random.Next(3, 8);
            var business = "";
            
            for (int i = 0; i < count; i++)
            {
                business += monkeyEmojis[random.Next(monkeyEmojis.Length)] + " ";
            }
            
            return business.Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting monkey business: {ex.Message}");
            return "🐒 Monkey business unavailable!";
        }
    }

    /// <summary>
    /// Gets statistics about the monkey collection
    /// </summary>
    /// <returns>Dictionary with collection statistics</returns>
    public static async Task<Dictionary<string, object>> GetMonkeyStatsAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        
        return new Dictionary<string, object>
        {
            ["Total Monkeys"] = monkeys.Count,
            ["Endangered Species"] = monkeys.Count(m => m.IsEndangered()),
            ["Average Population"] = monkeys.Any() ? (int)monkeys.Average(m => m.Population) : 0,
            ["Most Populated"] = monkeys.OrderByDescending(m => m.Population).FirstOrDefault()?.Name ?? "None",
            ["Least Populated"] = monkeys.OrderBy(m => m.Population).FirstOrDefault()?.Name ?? "None",
            ["Unique Locations"] = monkeys.Select(m => m.Location).Distinct().Count()
        };
    }

    /// <summary>
    /// Searches monkeys by location
    /// </summary>
    /// <param name="location">Location to search for</param>
    /// <returns>List of monkeys in the specified location</returns>
    public static async Task<List<Monkey>> SearchMonkeysByLocationAsync(string location)
    {
        var monkeys = await GetAllMonkeysAsync();
        return monkeys.Where(m => 
            m.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Gets sample monkeys data (placeholder for MCP integration)
    /// </summary>
    /// <returns>Sample list of monkeys</returns>
    private static List<Monkey> GetSampleMonkeys()
    {
        return new List<Monkey>
        {
            new Monkey
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg",
                Population = 10000,
                Latitude = -8.783195,
                Longitude = 34.508523
            },
            new Monkey
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg",
                Population = 23000,
                Latitude = 12.769013,
                Longitude = -85.602364
            },
            new Monkey
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/bluemonkey.jpg",
                Population = 12000,
                Latitude = 1.957709,
                Longitude = 37.297204
            },
            new Monkey
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/saimiri.jpg",
                Population = 11000,
                Latitude = -8.783195,
                Longitude = -55.491477
            },
            new Monkey
            {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/tamarin.jpg",
                Population = 19000,
                Latitude = -14.235004,
                Longitude = -51.92528
            },
            new Monkey
            {
                Name = "Japanese Macaque",
                Location = "Japan",
                Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/macasa.jpg",
                Population = 1000,
                Latitude = 36.204824,
                Longitude = 138.252924
            },
            new Monkey
            {
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, and Congo",
                Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/mandrill.jpg",
                Population = 17000,
                Latitude = 7.369722,
                Longitude = 12.354722
            },
            new Monkey
            {
                Name = "Proboscis Monkey",
                Location = "Borneo",
                Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/borneo.jpg",
                Population = 15000,
                Latitude = 0.961883,
                Longitude = 114.55485
            }
        };
    }

    /// <summary>
    /// Gets endangered monkeys (population below threshold)
    /// </summary>
    /// <returns>List of endangered monkeys</returns>
    public static async Task<List<Monkey>> GetEndangeredMonkeysAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        return monkeys.Where(m => m.IsEndangered()).ToList();
    }

    /// <summary>
    /// Gets monkeys sorted by population (ascending or descending)
    /// </summary>
    /// <param name="descending">True for descending order, false for ascending</param>
    /// <returns>Sorted list of monkeys</returns>
    public static async Task<List<Monkey>> GetMonkeysByPopulationAsync(bool descending = true)
    {
        var monkeys = await GetAllMonkeysAsync();
        return descending 
            ? monkeys.OrderByDescending(m => m.Population).ToList()
            : monkeys.OrderBy(m => m.Population).ToList();
    }

    /// <summary>
    /// Gets a random endangered monkey
    /// </summary>
    /// <returns>A random endangered monkey or null if none exist</returns>
    public static async Task<Monkey?> GetRandomEndangeredMonkeyAsync()
    {
        var endangeredMonkeys = await GetEndangeredMonkeysAsync();
        if (endangeredMonkeys.Count == 0)
            return null;
        
        return endangeredMonkeys[random.Next(endangeredMonkeys.Count)];
    }

    /// <summary>
    /// Finds monkeys by partial name match
    /// </summary>
    /// <param name="partialName">Partial name to search for</param>
    /// <returns>List of monkeys matching the partial name</returns>
    public static async Task<List<Monkey>> FindMonkeysByPartialNameAsync(string partialName)
    {
        var monkeys = await GetAllMonkeysAsync();
        return monkeys.Where(m => 
            m.Name.Contains(partialName, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Gets unique locations where monkeys are found
    /// </summary>
    /// <returns>List of unique locations</returns>
    public static async Task<List<string>> GetUniqueLocationsAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        return monkeys.Select(m => m.Location).Distinct().OrderBy(l => l).ToList();
    }

    /// <summary>
    /// Gets the most and least populated monkey species
    /// </summary>
    /// <returns>Tuple with most and least populated monkeys</returns>
    public static async Task<(Monkey? MostPopulated, Monkey? LeastPopulated)> GetPopulationExtremesAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        if (monkeys.Count == 0)
            return (null, null);
        
        var mostPopulated = monkeys.OrderByDescending(m => m.Population).First();
        var leastPopulated = monkeys.OrderBy(m => m.Population).First();
        
        return (mostPopulated, leastPopulated);
    }

    /// <summary>
    /// Clears the cached monkeys data
    /// </summary>
    public static void ClearCache()
    {
        cachedMonkeys = null;
    }

    /// <summary>
    /// Gets a sample journey for a monkey (placeholder for MCP integration)
    /// </summary>
    /// <param name="monkeyName">Name of the monkey</param>
    /// <returns>Sample journey data</returns>
    private static MonkeyJourney GetSampleJourney(string monkeyName)
    {
        return new MonkeyJourney
        {
            MonkeyName = monkeyName,
            StartLocation = new Location { Latitude = -8.783195, Longitude = 34.508523 },
            StartTime = DateTime.Now.AddHours(-2),
            EndTime = DateTime.Now,
            TotalDuration = TimeSpan.FromHours(2),
            TotalDistanceKm = 1.5,
            Activities = new List<MonkeyActivity>
            {
                new MonkeyActivity
                {
                    Type = "Foraging",
                    Description = "Found delicious fruits and ate them",
                    Location = new Location { Latitude = -8.783200, Longitude = 34.508500 },
                    Timestamp = DateTime.Now.AddHours(-1),
                    Duration = TimeSpan.FromMinutes(30),
                    EnergyChange = 15
                },
                new MonkeyActivity
                {
                    Type = "Grooming",
                    Description = "Spent time grooming with family",
                    Location = new Location { Latitude = -8.783180, Longitude = 34.508600 },
                    Timestamp = DateTime.Now.AddMinutes(-30),
                    Duration = TimeSpan.FromMinutes(20),
                    EnergyChange = 5
                }
            },
            HealthStats = new HealthStats
            {
                Energy = random.Next(70, 100),
                Happiness = random.Next(80, 100),
                Hunger = random.Next(20, 50),
                Social = random.Next(70, 100),
                Stress = random.Next(10, 30),
                Health = random.Next(85, 100)
            }
        };
    }
}