namespace DemoConsole._7_CallerArgumentExpression;

public static class Program
{
    public static void Main()
    {
        WriteLine();

        try
        {
            new List<int?>().Where(l => l % 2 == 0).FirstOrDefault(l => l > 0).IsNotNull();
        }
        catch (NullReferenceException nullEx)
        {
            WriteLine(nullEx.Message);
        }
    }
}

public static class NullCheckAssertions
{
    // FEATURE: Ability to get the serialized form of the "source expression" for a given method parameter
    public static void IsNotNull<T>(this T subject, [CallerArgumentExpression("subject")] string? subjectExpression = null)
    {
        if (subject is null)
        {
            throw new NullReferenceException($"'{subjectExpression}' resulted in null");
        }
    }
}