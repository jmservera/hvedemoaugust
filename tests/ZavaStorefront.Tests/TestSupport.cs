using System.Collections.Concurrent;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace ZavaStorefront.Tests;

internal sealed class TestSession : ISession
{
    private readonly Dictionary<string, byte[]> _values = new();

    public string Id => "test-session-not-for-logs";
    public bool IsAvailable => true;
    public IEnumerable<string> Keys => _values.Keys;

    public void Clear() => _values.Clear();
    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public void Remove(string key) => _values.Remove(key);
    public void Set(string key, byte[] value) => _values[key] = value;
    public bool TryGetValue(string key, out byte[] value) => _values.TryGetValue(key, out value!);
}

internal sealed record CapturedLog(string Category, LogLevel Level,
    IReadOnlyDictionary<string, object?> Fields, string Message);

internal sealed class TestLogProvider : ILoggerProvider
{
    public ConcurrentQueue<CapturedLog> Entries { get; } = new();

    public ILogger CreateLogger(string categoryName) => new CaptureLogger(categoryName, Entries);
    public void Dispose() { }

    private sealed class CaptureLogger : ILogger
    {
        private readonly string _category;
        private readonly ConcurrentQueue<CapturedLog> _entries;

        public CaptureLogger(string category, ConcurrentQueue<CapturedLog> entries)
        {
            _category = category;
            _entries = entries;
        }

        public IDisposable BeginScope<TState>(TState state) => new EmptyScope();
        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Warning;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (IsEnabled(logLevel) && _category == "ZavaStorefront.Services.CartService"
                && state is IEnumerable<KeyValuePair<string, object?>> fields)
            {
                _entries.Enqueue(new CapturedLog(_category, logLevel, fields.ToDictionary(p => p.Key, p => p.Value),
                    formatter(state, exception)));
            }
        }

        private sealed class EmptyScope : IDisposable
        {
            public void Dispose() { }
        }
    }
}

public sealed class StorefrontFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}
