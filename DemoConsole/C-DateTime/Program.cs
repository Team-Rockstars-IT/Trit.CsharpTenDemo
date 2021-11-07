namespace Trit.DemoConsole.C_DateTime;

public static class Demo
{
    public static Task Main()
    {
        // FEATURE: Addition of DateOnly and TimeOnly to framework
        var dateOnly = new DateOnly(1970, 1, 1);
        var timeOnly = new TimeOnly(14, 42, 6, 66);

        WriteLine($"Date: {dateOnly}, " +
                  $"{dateOnly:O}, " +
                  $"{dateOnly:R}, " +
                  $"{dateOnly:dd}-{dateOnly:MM}-{dateOnly:yy}" +
                  $"\nTime: {timeOnly}, " +
                  $"{timeOnly:R}, " +
                  $"{timeOnly:O}, " +
                  $"{timeOnly:HH}:{timeOnly:mm}:" +
                  $"{timeOnly:ffff}");




        try
        {
            // DateOnly heavily leans on DateTime to determine
            // dayNumber and for formatting...
            new DateOnly(year: 50000, 1, 1);
        }
        catch (ArgumentOutOfRangeException)
        {
            // Causing it to be leaky abstraction :(
        }

        return Task.CompletedTask;
    }
}