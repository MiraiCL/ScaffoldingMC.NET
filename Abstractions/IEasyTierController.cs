using ScaffoldingMC.Models;

namespace ScaffoldingMC.Abstractions;

public interface IEasyTierController{
    void Startup(IEnumerable<string> argument);
    IEnumerable<PeerInfo>? GetPeerInformation();
    
}