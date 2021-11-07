// ReSharper disable MethodSupportsCancellation
namespace Trit.DemoConsole.E_Timer;

public static class Demo
{
    public static async Task Main()
    {
        var delaySource = new CancellationTokenSource(
            TimeSpan.FromSeconds(5));
        using var timer = new PeriodicTimer(
            TimeSpan.FromSeconds(1));

        // FEATURE: Yet another timer :D
        //          But this time it's async and awaitable
        while (await timer.WaitForNextTickAsync())
        {
            WriteLine($"Clearing browser history @ {DateTime.Now}");

            if (delaySource.Token.IsCancellationRequested)
            {
                WriteLine("Done");
                break;
            }
        }
    }
}