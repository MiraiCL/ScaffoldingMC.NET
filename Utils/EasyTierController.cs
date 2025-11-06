using System.Diagnostics;
using ScaffoldingMC.Abstractions;
using ScaffoldingMC.Models;

namespace ScaffoldingMC.Utils;

public sealed class EasyTierController(string corePath,string cliPath):IEasyTierController,IDisposable{
    private Process _easyTierProcess = new();
    public bool Working;
    public bool Disposed;

    private object _syncLock = new();

    public void Startup(IEnumerable<string> argument){
        lock(_syncLock){
            if(Disposed) throw new ObjectDisposedException("Current controller already disposed.");
            if(Working) return;
            _easyTierProcess.StartInfo.FileName = corePath;
            foreach(var arg in argument){
                var argFixed = arg;
                if(arg.Contains(" ")) argFixed = $"""{arg.Replace("\"","\\\"")}""";
                _easyTierProcess.StartInfo.Arguments += $" {argFixed}";
            }
            _easyTierProcess.StartInfo.CreateNoWindow = true;
            _easyTierProcess.Start();
            Working = true;
        }
    }

    public void Stop(){
        _easyTierProcess.Kill();
        _easyTierProcess.WaitForExit(1000);
    }

    public IEnumerable<PeerInfo>? GetPeerInformation(){
        //resharper disable all
        using var process = new Process{
            StartInfo = new ProcessStartInfo{
                FileName = cliPath,Arguments = "-o json peer",
                RedirectStandardOutput = true
            }
        };
        //resharper restore all
        return PeerInfo.Parse(process.StandardOutput.ReadToEnd());
    }

    public void Dispose()
    {
        Stop();
        _easyTierProcess.Dispose(); 
        Disposed = true;       
    }
}