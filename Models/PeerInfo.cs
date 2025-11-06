using System.Text.Json.Nodes;

namespace ScaffoldingMC.Models;

public record PeerInfo{

    public string? Cidr {get;set;}
    public string? IPv4 {get;set;}
    public string? HostName {get;set;}
    public required string ConntectionMethod {get;set;}
    public double ConnectionDelay {get;set;}
    public double LossRate {get;set;}
    public double Upload {get;set;}
    public double Download {get;set;}


    public static IEnumerable<PeerInfo>? Parse(string data){
        if(data.Contains("|")) throw new ArgumentException("This json contains invalid token.");
        var json = JsonNode.Parse(data);
        return json?.AsArray().Select(j => new PeerInfo{
            Cidr = j?["cidr"]?.ToString(),
            ConnectionDelay = j?["lat_ms"]?.GetValue<double>() ?? 0.0,
            ConntectionMethod = j?["tunnel_proto"]?.ToString() ?? "Unknown",Download = j?["rx_bytes"]?.GetValue<double>() ?? 0.0,
            HostName = j?["host_name"]?.ToString(),
            IPv4 = j?["ipv4"]?.ToString(),
            LossRate = j?["loss_rate"]?.GetValue<double>() ?? 0.0,
            Upload = j?["tx_bytes"]?.GetValue<double>() ?? 0.0
        });
    }
}
