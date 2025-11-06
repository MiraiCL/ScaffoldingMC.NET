using System.Net;
using System.Net.Sockets;
using ScaffoldingMC.Abstractions;

namespace ScaffoldingMC.Utils;

public sealed class LobbyServer:ILobbyServer{
    private readonly TcpListener _listener = new(IPAddress.Any,0);
    private Task? _listenThread;
    public event Action<string> OnDataRecevied;
    public void Startup(){
        
    }

    public int GetCurrentPort() => ((IPEndPoint)_listener.Server.LocalEndPoint).Port;

    
}