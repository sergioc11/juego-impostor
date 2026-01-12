using System.Collections.Concurrent;

public class GameState
{
    // Thread-safe collection for players
    public ConcurrentDictionary<string, Player> Players { get; } = new();
    
    public string CurrentTheme { get; set; } = string.Empty;
    public int ImpostorCount { get; set; }
    public bool IsGameActive { get; set; } = false;
    
    // Store used themes to avoid repetition
    public HashSet<string> UsedThemes { get; } = new();

    public class Player
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; } // To track who initiated/can start
    }
}
