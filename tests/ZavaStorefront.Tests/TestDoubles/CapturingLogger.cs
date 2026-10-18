using Microsoft.Extensions.Logging;

namespace ZavaStorefront.Tests.TestDoubles;

/// <summary>
/// One captured log call: level, rendered message, and the structured state key/value pairs.
/// </summary>
internal sealed record LogEntry(
    LogLevel Level,
    string Message,
    IReadOnlyList<KeyValuePair<string, object?>> State)
{
    /// <summary>
    /// Gets the named properties of the structured state, excluding the framework-added original-format entry.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Properties =>
        State.Where(pair => pair.Key != "{OriginalFormat}")
             .ToDictionary(pair => pair.Key, pair => pair.Value);
}

/// <summary>
/// Hand-rolled logger that records every entry at every level for assertions.
/// </summary>
internal sealed class CapturingLogger<T> : ILogger<T>
{
    public List<LogEntry> Entries { get; } = new();

    public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var pairs = state is IEnumerable<KeyValuePair<string, object?>> kvps
            ? kvps.ToList()
            : new List<KeyValuePair<string, object?>>();
        Entries.Add(new LogEntry(logLevel, formatter(state, exception), pairs));
    }

    private sealed class NullScope : IDisposable
    {
        public static readonly NullScope Instance = new();

        public void Dispose()
        {
        }
    }
}
