using MyMonkeyApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MyMonkeyApp.Helpers;

/// <summary>
/// Helper class for integrating with MonkeyMCP server
/// </summary>
public static class MCPMonkeyClient
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Gets all monkeys from the MCP server
    /// </summary>
    /// <returns>List of monkeys from MCP</returns>
    public static async Task<List<Monkey>> GetMonkeysFromMCPAsync()
    {
        try
        {
            // This would normally be done through proper MCP client integration
            // For now, we'll simulate the MCP server response with actual data
            var mcpResponse = await SimulateMCPCall("get_monkeys");
            
            if (string.IsNullOrEmpty(mcpResponse))
            {
                return new List<Monkey>();
            }

            var monkeys = JsonSerializer.Deserialize<List<Monkey>>(mcpResponse, jsonOptions);
            return monkeys ?? new List<Monkey>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling MCP server: {ex.Message}");
            return new List<Monkey>();
        }
    }

    /// <summary>
    /// Gets a specific monkey from the MCP server
    /// </summary>
    /// <param name="name">Name of the monkey to get</param>
    /// <returns>The monkey data from MCP</returns>
    public static async Task<Monkey?> GetMonkeyFromMCPAsync(string name)
    {
        try
        {
            var mcpResponse = await SimulateMCPCall($"get_monkey:{name}");
            
            if (string.IsNullOrEmpty(mcpResponse))
            {
                return null;
            }

            var monkey = JsonSerializer.Deserialize<Monkey>(mcpResponse, jsonOptions);
            return monkey;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting monkey {name} from MCP: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets a monkey journey from the MCP server
    /// </summary>
    /// <param name="monkeyName">Name of the monkey</param>
    /// <returns>Journey data from MCP</returns>
    public static async Task<MonkeyJourney?> GetMonkeyJourneyFromMCPAsync(string monkeyName)
    {
        try
        {
            var mcpResponse = await SimulateMCPCall($"get_monkey_journey:{monkeyName}");
            
            if (string.IsNullOrEmpty(mcpResponse))
            {
                return null;
            }

            var journey = JsonSerializer.Deserialize<MonkeyJourney>(mcpResponse, jsonOptions);
            return journey;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting journey for {monkeyName} from MCP: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets monkey business from the MCP server
    /// </summary>
    /// <returns>Monkey business string from MCP</returns>
    public static async Task<string?> GetMonkeyBusinessFromMCPAsync()
    {
        try
        {
            var mcpResponse = await SimulateMCPCall("get_monkey_business");
            return mcpResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting monkey business from MCP: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Simulates an MCP server call (placeholder for actual MCP integration)
    /// In a real implementation, this would use proper MCP client libraries
    /// </summary>
    /// <param name="command">The MCP command to simulate</param>
    /// <returns>Simulated MCP response</returns>
    private static async Task<string?> SimulateMCPCall(string command)
    {
        // Simulate network delay
        await Task.Delay(100);

        return command switch
        {
            "get_monkeys" => GetMockMonkeysResponse(),
            var cmd when cmd.StartsWith("get_monkey:") => GetMockMonkeyResponse(cmd.Split(':')[1]),
            var cmd when cmd.StartsWith("get_monkey_journey:") => GetMockJourneyResponse(cmd.Split(':')[1]),
            "get_monkey_business" => GetMockMonkeyBusinessResponse(),
            _ => null
        };
    }

    /// <summary>
    /// Returns mock monkeys response
    /// </summary>
    /// <returns>JSON string of monkeys</returns>
    private static string GetMockMonkeysResponse()
    {
        var monkeys = new List<object>
        {
            new {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg",
                Population = 10000,
                Latitude = -8.783195,
                Longitude = 34.508523
            },
            new {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg",
                Population = 23000,
                Latitude = 12.769013,
                Longitude = -85.602364
            },
            new {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/tamarin.jpg",
                Population = 19000,
                Latitude = -14.235004,
                Longitude = -51.92528
            },
            new {
                Name = "Japanese Macaque",
                Location = "Japan",
                Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/macasa.jpg",
                Population = 1000,
                Latitude = 36.204824,
                Longitude = 138.252924
            },
            new {
                Name = "Sebastian",
                Location = "Seattle",
                Details = "This little trouble maker lives in Seattle with James and loves traveling on adventures with James and tweeting @MotzMonkeys.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/sebastian.jpg",
                Population = 1,
                Latitude = 47.606209,
                Longitude = -122.332071
            }
        };

        return JsonSerializer.Serialize(monkeys, new JsonSerializerOptions { WriteIndented = true });
    }

    /// <summary>
    /// Returns mock single monkey response
    /// </summary>
    /// <param name="name">Name of the monkey</param>
    /// <returns>JSON string of the monkey</returns>
    private static string? GetMockMonkeyResponse(string name)
    {
        var monkey = name.ToLower() switch
        {
            "baboon" => new {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg",
                Population = 10000,
                Latitude = -8.783195,
                Longitude = 34.508523
            },
            "sebastian" => new {
                Name = "Sebastian",
                Location = "Seattle",
                Details = "This little trouble maker lives in Seattle with James and loves traveling on adventures.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/sebastian.jpg",
                Population = 1,
                Latitude = 47.606209,
                Longitude = -122.332071
            },
            _ => null
        };

        return monkey != null ? JsonSerializer.Serialize(monkey) : null;
    }

    /// <summary>
    /// Returns mock journey response
    /// </summary>
    /// <param name="monkeyName">Name of the monkey</param>
    /// <returns>JSON string of the journey</returns>
    private static string GetMockJourneyResponse(string monkeyName)
    {
        var journey = new
        {
            MonkeyName = monkeyName,
            StartLocation = new { Latitude = -8.783195, Longitude = 34.508523 },
            StartTime = DateTime.Now.AddHours(-2),
            EndTime = DateTime.Now,
            TotalDuration = "02:00:00",
            TotalDistanceKm = 1.5,
            Activities = new[]
            {
                new {
                    Type = "Foraging",
                    Description = "Found delicious fruits and ate them",
                    Location = new { Latitude = -8.783200, Longitude = 34.508500 },
                    Timestamp = DateTime.Now.AddHours(-1),
                    Duration = "00:30:00",
                    EnergyChange = 15
                }
            },
            HealthStats = new
            {
                Energy = 88,
                Happiness = 91,
                Hunger = 73,
                Social = 102,
                Stress = 35,
                Health = 99
            }
        };

        return JsonSerializer.Serialize(journey, new JsonSerializerOptions { WriteIndented = true });
    }

    /// <summary>
    /// Returns mock monkey business response
    /// </summary>
    /// <returns>Monkey business string</returns>
    private static string GetMockMonkeyBusinessResponse()
    {
        var businesses = new[]
        {
            "🐒 🍌 🌴 🦧",
            "🙈 🙉 🙊 🐵",
            "🦧 🍌 🌺 🥥 🐒",
            "🐵 🌴 🍌 🐒 🦧",
            "🙊 🌺 🥥 🙈 🙉"
        };

        var random = new Random();
        return businesses[random.Next(businesses.Length)];
    }
}