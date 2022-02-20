namespace Trit.DemoConsole._2_Interpolated_strings;

public static class Demo
{
    internal const string DemoEnvironment = "CS10_DEMO_ENVIRONMENT";
    private const string FirstName = "Albert";
    private const string LastName = "Einstein";

    public static Task Main()
    {
        // FEATURE: Constant interpolated strings
        const string name = $"{FirstName} {LastName}";

        ILogger logger = new ConsoleLogger();
        logger.LogDebug($"Starting run for: {name}"); // This won't get logged
        Environment.SetEnvironmentVariable(DemoEnvironment, "Development");
        logger.LogDebug($"Done running for: {name}"); // But this will
        Environment.SetEnvironmentVariable(DemoEnvironment, "Production");

        return Task.CompletedTask;
    }
}

public static class LoggerExtensions
{
    // FEATURE: Interpolated string handlers are new
    //          and allow lowered interpolated string code
    //          to be more efficient
    public static void LogDebug(this ILogger logger,
        ref DebugMessageInterpolatedStringHandler handler)
    {
        handler.LogWith(logger);
    }
}

/// <summary>
/// This string handler will only allocate new strings
/// and log a message if Debug logging is actually enabled
/// </summary>
[InterpolatedStringHandler]
public ref struct DebugMessageInterpolatedStringHandler
{
    private DefaultInterpolatedStringHandler _innerHandler;
    private readonly bool _shouldLog;

    public DebugMessageInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _innerHandler = new DefaultInterpolatedStringHandler(
            literalLength,
            formattedCount);

        _shouldLog = Environment
            .GetEnvironmentVariable(Demo.DemoEnvironment) == "Development";
    }

    public void AppendLiteral(string value)
    {
        if (_shouldLog)
        {
            _innerHandler.AppendLiteral(value);
        }
    }

    public void AppendFormatted(string? value)
    {
        if (_shouldLog)
        {
            _innerHandler.AppendFormatted(value);
        }
    }

    // Note that quite a few Append... overloads are missing here
    // This is purely done for the sake of brevity

    public void LogWith(ILogger logger)
    {
        if (_shouldLog)
        {
            logger.LogDebug(_innerHandler.ToStringAndClear());
        }
    }
}

#region Simple logger implementation

public class ConsoleLogger : ILogger, IDisposable
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        WriteLine($"[{logLevel}]: {formatter(state, exception)}");
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable BeginScope<TState>(TState state) => this;

    public void Dispose()
    {
    }
}

#endregion