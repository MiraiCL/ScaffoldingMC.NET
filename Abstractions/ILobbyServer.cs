namespace ScaffoldingMC.Abstractions;

public interface ILobbyServer{
    void Startup();
    int GetCurrentPort();
}