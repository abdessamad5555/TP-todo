using Microsoft.Extensions.Logging;
using System;
using System.IO;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;

    public FileLoggerProvider(string filePath)
    {
        _filePath = filePath;

        var dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    public ILogger CreateLogger(string categoryName) => new FileLogger(_filePath);

    public void Dispose() { }
}

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private static object _lock = new object();

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId,
                            TState state, Exception exception,
                            Func<TState, Exception, string> formatter)
    {
        string message =
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {logLevel} | {formatter(state, exception)}";

        lock (_lock)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }
}
