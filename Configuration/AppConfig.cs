namespace MyMonkeyApp.Configuration;

/// <summary>
/// Configuration settings for the Monkey Console Application
/// </summary>
public static class AppConfig
{
    /// <summary>
    /// Gets or sets whether to use the MCP server for data
    /// </summary>
    public static bool UseMCPServer { get; set; } = false;

    /// <summary>
    /// Gets or sets the MCP server endpoint
    /// </summary>
    public static string MCPServerEndpoint { get; set; } = "localhost:8080";

    /// <summary>
    /// Gets or sets the maximum number of random monkeys that can be selected at once
    /// </summary>
    public static int MaxRandomMonkeys { get; set; } = 10;

    /// <summary>
    /// Gets or sets whether to show detailed monkey information by default
    /// </summary>
    public static bool ShowDetailedInfo { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use colored console output
    /// </summary>
    public static bool UseColoredOutput { get; set; } = true;

    /// <summary>
    /// Gets or sets the application title
    /// </summary>
    public static string AppTitle { get; set; } = "MyMonkey App";

    /// <summary>
    /// Gets or sets the welcome message
    /// </summary>
    public static string WelcomeMessage { get; set; } = "Descubre el fascinante mundo de los monos";

    /// <summary>
    /// ASCII art for the application header
    /// </summary>
    public static string AppAsciiArt { get; } = @"
    🐒 MyMonkey App 🐒
   ==================
    Welcome to the 
   Monkey Management
      Console App!
";

    /// <summary>
    /// Goodbye ASCII art
    /// </summary>
    public static string GoodbyeAsciiArt { get; } = @"
    👋 ¡Hasta luego! 👋
   ==================
   Gracias por usar
    MyMonkey App!
";

    /// <summary>
    /// Gets the population threshold for considering a species endangered
    /// </summary>
    public static int EndangeredPopulationThreshold { get; } = 5000;

    /// <summary>
    /// Gets the default console colors for different elements
    /// </summary>
    public static class Colors
    {
        public static ConsoleColor Header { get; } = ConsoleColor.Yellow;
        public static ConsoleColor Menu { get; } = ConsoleColor.Cyan;
        public static ConsoleColor MonkeyName { get; } = ConsoleColor.Magenta;
        public static ConsoleColor Success { get; } = ConsoleColor.Green;
        public static ConsoleColor Error { get; } = ConsoleColor.Red;
        public static ConsoleColor Info { get; } = ConsoleColor.Gray;
    }

    /// <summary>
    /// Menu options with emojis
    /// </summary>
    public static class MenuOptions
    {
        public static readonly Dictionary<int, string> MainMenu = new()
        {
            { 1, "🎲 1. Mostrar mono aleatorio" },
            { 2, "🎯 2. Mostrar múltiples monos aleatorios" },
            { 3, "📋 3. Listar todos los monos" },
            { 4, "🔍 4. Buscar mono por nombre" },
            { 5, "🗺️  5. Mono aleatorio con viaje" },
            { 6, "🎪 6. Monkey Business (emojis)" },
            { 7, "📊 7. Estadísticas de monos" },
            { 8, "🌍 8. Buscar por ubicación" },
            { 0, "🚪 0. Salir" }
        };
    }

    /// <summary>
    /// Emoji collections for different purposes
    /// </summary>
    public static class Emojis
    {
        public static readonly string[] MonkeyEmojis = { "🐒", "🐵", "🙈", "🙉", "🙊", "🦧" };
        public static readonly string[] NatureEmojis = { "🍌", "🌴", "🥥", "🌺", "🌿", "🍃" };
        public static readonly string[] ActivityEmojis = { "🏃", "🧗", "🍽️", "😴", "👥", "🔍" };
        
        public static string[] AllMonkeyRelated => MonkeyEmojis.Concat(NatureEmojis).ToArray();
    }

    /// <summary>
    /// Application version information
    /// </summary>
    public static class Version
    {
        public static string AppVersion { get; } = "1.0.0";
        public static string BuildDate { get; } = DateTime.Now.ToString("yyyy-MM-dd");
        public static string Description { get; } = "Console application for managing monkey species data";
    }
}