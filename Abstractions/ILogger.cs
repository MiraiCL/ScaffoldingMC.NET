namespace ScaffoldingMC.Abstractions;

public interface ILogger{
    void Trace(string module,string message);
    void Trace(Exception ex,string module,string message);
    void Debug(string module,string message);
    void Debug(Exception ex,string module,string message);
    void Info(string module,string message);
    void Info(Exception ex,string module,string message);
    void Warning(string module,string message);
    void Warning(Exception ex,string module,string message);
    void Error(string module,string message);
    void Error(Exception ex,string module,string message);
}