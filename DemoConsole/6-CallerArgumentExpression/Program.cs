namespace Trit.DemoConsole._6_CallerArgumentExpression;

public static class Demo
{
    public static Task Main()
    {
        try
        {
            new List<int?>()
                .Where(l => l % 2 == 0)
                .FirstOrDefault(l => l > 0)
                .IsNotNull();
        }
        catch (NullReferenceException nullEx)
        {
            WriteLine(nullEx.Message);
        }

        return Task.CompletedTask;
    }
}

public static class NullCheckAssertions
{
    // FEATURE: Ability to get the serialized form
    //          of the "source expression" for a given method
    //          parameter
    public static void IsNotNull<T>(this T subject,
        [CallerArgumentExpression("subject")] string? expression = null)
    {
        if (subject is null)
        {
            throw new NullReferenceException(
                $"'{expression}' resulted in null");
        }
    }
}