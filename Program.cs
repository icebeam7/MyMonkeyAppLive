using MyMonkeyApp.Helpers;
using MyMonkeyApp.Models;

namespace MyMonkeyApp;

/// <summary>
/// Main program class for the Monkey Console Application
/// </summary>
class Program
{
    private static readonly Random random = new();

    /// <summary>
    /// Main entry point of the application
    /// </summary>
    /// <param name="args">Command line arguments</param>
    static async Task Main(string[] args)
    {
        await RunApplication();
    }

    /// <summary>
    /// Runs the main application loop
    /// </summary>
    private static async Task RunApplication()
    {
        ShowWelcomeMessage();

        while (true)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice?.ToLower())
                {
                    case "1":
                        await ShowRandomMonkey();
                        break;
                    case "2":
                        await ShowMultipleRandomMonkeys();
                        break;
                    case "3":
                        await ShowAllMonkeys();
                        break;
                    case "4":
                        await SearchMonkeyByName();
                        break;
                    case "5":
                        await ShowRandomMonkeyWithJourney();
                        break;
                    case "6":
                        await ShowMonkeyBusiness();
                        break;
                    case "7":
                        await ShowMonkeyStatistics();
                        break;
                    case "8":
                        await SearchMonkeysByLocation();
                        break;
                    case "0" or "q" or "quit" or "exit":
                        ShowGoodbyeMessage();
                        return;
                    default:
                        Console.WriteLine("❌ Opción no válida. Por favor, selecciona una opción del menú.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            if (choice != "0" && !string.Equals(choice, "quit", StringComparison.OrdinalIgnoreCase) && 
                !string.Equals(choice, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n🔄 Presiona Enter para continuar...");
                Console.ReadLine();
            }
        }
    }

    /// <summary>
    /// Displays the welcome message with ASCII art
    /// </summary>
    private static void ShowWelcomeMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
    🐒 MyMonkey App 🐒
   ==================
    Welcome to the 
   Monkey Management
      Console App!
");
        Console.ResetColor();
        Console.WriteLine("🌟 Descubre el fascinante mundo de los monos 🌟\n");
    }

    /// <summary>
    /// Displays the main menu options
    /// </summary>
    private static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine("           MENÚ PRINCIPAL");
        Console.WriteLine("═══════════════════════════════════════");
        Console.ResetColor();
        
        Console.WriteLine("🎲 1. Mostrar mono aleatorio");
        Console.WriteLine("🎯 2. Mostrar múltiples monos aleatorios");
        Console.WriteLine("📋 3. Listar todos los monos");
        Console.WriteLine("🔍 4. Buscar mono por nombre");
        Console.WriteLine("🗺️  5. Mono aleatorio con viaje");
        Console.WriteLine("🎪 6. Monkey Business (emojis)");
        Console.WriteLine("📊 7. Estadísticas de monos");
        Console.WriteLine("🌍 8. Buscar por ubicación");
        Console.WriteLine("🚪 0. Salir");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("═══════════════════════════════════════");
        Console.ResetColor();
        Console.Write("Selecciona una opción: ");
    }

    /// <summary>
    /// Shows a single random monkey
    /// </summary>
    private static async Task ShowRandomMonkey()
    {
        Console.WriteLine("\n🎲 Seleccionando mono aleatorio...");
        
        var monkey = await MonkeyHelper.GetRandomMonkeyAsync();
        if (monkey != null)
        {
            DisplayMonkeyDetails(monkey);
        }
        else
        {
            Console.WriteLine("❌ No se pudo obtener un mono aleatorio.");
        }
    }

    /// <summary>
    /// Shows multiple random monkeys
    /// </summary>
    private static async Task ShowMultipleRandomMonkeys()
    {
        Console.Write("¿Cuántos monos aleatorios deseas ver? (1-10): ");
        if (int.TryParse(Console.ReadLine(), out int count) && count >= 1 && count <= 10)
        {
            Console.WriteLine($"\n🎯 Seleccionando {count} monos aleatorios...");
            
            var monkeys = await MonkeyHelper.GetMultipleRandomMonkeysAsync(count);
            
            for (int i = 0; i < monkeys.Count; i++)
            {
                Console.WriteLine($"\n--- Mono #{i + 1} ---");
                DisplayMonkeyDetails(monkeys[i]);
            }
        }
        else
        {
            Console.WriteLine("❌ Número no válido. Debe ser entre 1 y 10.");
        }
    }

    /// <summary>
    /// Shows all available monkeys
    /// </summary>
    private static async Task ShowAllMonkeys()
    {
        Console.WriteLine("\n📋 Cargando todos los monos...");
        
        var monkeys = await MonkeyHelper.GetAllMonkeysAsync();
        
        Console.WriteLine($"\n🐒 Total de monos disponibles: {monkeys.Count}\n");
        
        for (int i = 0; i < monkeys.Count; i++)
        {
            Console.WriteLine($"{i + 1:D2}. {monkeys[i]}");
        }
    }

    /// <summary>
    /// Searches for a monkey by name
    /// </summary>
    private static async Task SearchMonkeyByName()
    {
        Console.Write("\n🔍 Ingresa el nombre del mono a buscar: ");
        var name = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("❌ Nombre no válido.");
            return;
        }
        
        var monkey = await MonkeyHelper.GetMonkeyByNameAsync(name);
        if (monkey != null)
        {
            Console.WriteLine($"\n✅ Mono encontrado: {name}");
            DisplayMonkeyDetails(monkey);
        }
        else
        {
            Console.WriteLine($"❌ No se encontró un mono con el nombre '{name}'.");
        }
    }

    /// <summary>
    /// Shows a random monkey with its journey data
    /// </summary>
    private static async Task ShowRandomMonkeyWithJourney()
    {
        Console.WriteLine("\n🗺️ Seleccionando mono aleatorio con viaje...");
        
        var monkey = await MonkeyHelper.GetRandomMonkeyAsync();
        if (monkey == null)
        {
            Console.WriteLine("❌ No se pudo obtener un mono aleatorio.");
            return;
        }
        
        DisplayMonkeyDetails(monkey);
        
        var journey = await MonkeyHelper.GetMonkeyJourneyAsync(monkey.Name);
        if (journey != null)
        {
            Console.WriteLine("\n🗺️ === INFORMACIÓN DEL VIAJE ===");
            Console.WriteLine(journey.ToString());
            
            Console.WriteLine($"\n📊 Estadísticas del viaje:");
            var stats = journey.GetJourneyStats();
            foreach (var stat in stats)
            {
                Console.WriteLine($"   {stat.Key}: {stat.Value}");
            }
            
            Console.WriteLine($"\n💪 Estado de salud:");
            Console.WriteLine($"   {journey.HealthStats}");
        }
    }

    /// <summary>
    /// Shows monkey business emojis
    /// </summary>
    private static async Task ShowMonkeyBusiness()
    {
        Console.WriteLine("\n🎪 Monkey Business en progreso...");
        
        var business = await MonkeyHelper.GetMonkeyBusinessAsync();
        if (!string.IsNullOrEmpty(business))
        {
            Console.WriteLine($"\n🎭 {business}");
        }
        else
        {
            Console.WriteLine("❌ No se pudo obtener Monkey Business.");
        }
    }

    /// <summary>
    /// Shows comprehensive monkey statistics
    /// </summary>
    private static async Task ShowMonkeyStatistics()
    {
        Console.WriteLine("\n📊 Cargando estadísticas de monos...");
        
        var stats = await MonkeyHelper.GetMonkeyStatsAsync();
        var extremes = await MonkeyHelper.GetPopulationExtremesAsync();
        var endangered = await MonkeyHelper.GetEndangeredMonkeysAsync();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n📈 === ESTADÍSTICAS DE MONOS ===");
        Console.ResetColor();
        
        Console.WriteLine($"🐒 Total de especies: {stats["Total Monkeys"]}");
        Console.WriteLine($"⚠️  Especies en peligro: {stats["Endangered Species"]}");
        Console.WriteLine($"👥 Población promedio: {stats["Average Population"]:N0}");
        Console.WriteLine($"🌍 Ubicaciones únicas: {stats["Unique Locations"]}");
        
        if (extremes.MostPopulated != null)
        {
            Console.WriteLine($"🥇 Más poblado: {extremes.MostPopulated.Name} ({extremes.MostPopulated.Population:N0})");
        }
        
        if (extremes.LeastPopulated != null)
        {
            Console.WriteLine($"🥉 Menos poblado: {extremes.LeastPopulated.Name} ({extremes.LeastPopulated.Population:N0})");
        }
        
        if (endangered.Count > 0)
        {
            Console.WriteLine($"\n⚠️  Especies en peligro:");
            foreach (var monkey in endangered)
            {
                Console.WriteLine($"   • {monkey.Name} - {monkey.Population:N0} individuos");
            }
        }
    }

    /// <summary>
    /// Searches for monkeys by location
    /// </summary>
    private static async Task SearchMonkeysByLocation()
    {
        Console.Write("\n🌍 Ingresa la ubicación a buscar: ");
        var location = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrWhiteSpace(location))
        {
            Console.WriteLine("❌ Ubicación no válida.");
            return;
        }
        
        var monkeys = await MonkeyHelper.SearchMonkeysByLocationAsync(location);
        
        if (monkeys.Count > 0)
        {
            Console.WriteLine($"\n✅ Encontrados {monkeys.Count} mono(s) en ubicaciones que contienen '{location}':");
            
            for (int i = 0; i < monkeys.Count; i++)
            {
                Console.WriteLine($"\n--- Mono #{i + 1} ---");
                DisplayMonkeyDetails(monkeys[i]);
            }
        }
        else
        {
            Console.WriteLine($"❌ No se encontraron monos en ubicaciones que contengan '{location}'.");
            
            // Show available locations
            var locations = await MonkeyHelper.GetUniqueLocationsAsync();
            Console.WriteLine($"\n🌍 Ubicaciones disponibles:");
            foreach (var loc in locations)
            {
                Console.WriteLine($"   • {loc}");
            }
        }
    }

    /// <summary>
    /// Displays detailed information about a monkey
    /// </summary>
    /// <param name="monkey">The monkey to display</param>
    private static void DisplayMonkeyDetails(Monkey monkey)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n🐒 === {monkey.Name.ToUpper()} ===");
        Console.ResetColor();
        
        Console.WriteLine($"📍 Ubicación: {monkey.Location}");
        Console.WriteLine($"👥 Población: {monkey.Population:N0}{(monkey.IsEndangered() ? " (⚠️ En peligro)" : "")}");
        Console.WriteLine($"🌍 Coordenadas: {monkey.GetCoordinates()}");
        Console.WriteLine($"🖼️ Imagen: {(string.IsNullOrEmpty(monkey.Image) ? "No disponible" : monkey.Image)}");
        
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"\n📝 Detalles:\n{monkey.Details}");
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the goodbye message
    /// </summary>
    private static void ShowGoodbyeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
    👋 ¡Hasta luego! 👋
   ==================
   Gracias por usar
    MyMonkey App!
");
        Console.ResetColor();
    }
}
