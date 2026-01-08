using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    private readonly GameState _gameState;

    public GameHub(GameState gameState)
    {
        _gameState = gameState;
    }

    private static readonly List<string> PredefinedThemes = new()
    {
        "Hospital", "Escuela", "Aeropuerto", "Banco", "Restaurante", 
        "Cine", "Estación de Policía", "Supermercado", "Gimnasio", "Hotel",
        "Base Militar", "Circo", "Casino", "Universidad", "Playa",
        "Zoológico", "Museo", "Biblioteca", "Barco Pirata", "Estación Espacial",
        "Submarino Nuclear", "Mansión Embrujada", "Búnker Apocalíptico", "Laboratorio Secreto", "Fábrica de Chocolate",
        "Concierto de Rock", "Desfile de Modas", "Estudio de TV", "Parque de Atracciones", "Tren de Lujo"
    };

    public async Task JoinGame(string playerName)
    {
        var player = new GameState.Player
        {
            ConnectionId = Context.ConnectionId,
            Name = playerName,
            Role = "Lobby", // Default role
            IsAdmin = _gameState.Players.IsEmpty // First player is Admin
        };

        _gameState.Players.TryAdd(Context.ConnectionId, player);

        // Notify caller of their admin status
        await Clients.Caller.SendAsync("ReceiveJoinStatus", player.IsAdmin);

        // Notify all clients of the updated player list
        await NotifyPlayerListUpdate();
    }

    public async Task StartGame(string tema, int cantidadImpostores)
    {
        if (!_gameState.Players.TryGetValue(Context.ConnectionId, out var requestor) || !requestor.IsAdmin)
        {
            // Optional: Notify that only admin can start
            return;
        }

        if (_gameState.Players.Count < cantidadImpostores + 1)
        {
             // Not enough players logic could go here
        }

        if (string.IsNullOrWhiteSpace(tema))
        {
            var rngTheme = new Random();
            tema = PredefinedThemes[rngTheme.Next(PredefinedThemes.Count)];
        }

        _gameState.CurrentTheme = tema;
        _gameState.ImpostorCount = cantidadImpostores;
        _gameState.IsGameActive = true;

        var playerList = _gameState.Players.Values.ToList();
        
        // Shuffle players
        var rng = new Random();
        var shuffledPlayers = playerList.OrderBy(a => rng.Next()).ToList();

        // Assign Impostors
        for (int i = 0; i < shuffledPlayers.Count; i++)
        {
            if (i < cantidadImpostores)
            {
                shuffledPlayers[i].Role = "Impostor";
            }
            else
            {
                shuffledPlayers[i].Role = "Civilian"; // Or "Townsperson"
            }
        }

        // Notify each player individually
        foreach (var p in _gameState.Players.Values)
        {
            if (p.Role == "Impostor")
            {
                await Clients.Client(p.ConnectionId).SendAsync("GameStarted", "Eres el IMPOSTOR", "???", "Impostor");
            }
            else
            {
                await Clients.Client(p.ConnectionId).SendAsync("GameStarted", $"Tema: {tema}", tema, "Civilian");
            }
        }
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_gameState.Players.TryRemove(Context.ConnectionId, out _))
        {
            await NotifyPlayerListUpdate();
        }
        await base.OnDisconnectedAsync(exception);
    }

    private async Task NotifyPlayerListUpdate()
    {
        var players = _gameState.Players.Values.Select(p => new { p.Name, p.IsAdmin }).ToList();
        await Clients.All.SendAsync("UpdatePlayerList", players);
    }
}
