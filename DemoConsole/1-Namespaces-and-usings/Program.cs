// FEATURE: File scoped namespaces
namespace Trit.DemoConsole._1_Namespaces_and_usings;

public static class Demo
{
    public static Task Main()
    {
        var piString = QuickMaths.PI.ToString(InvariantCulture);

        var piBytes = Encoding.ASCII.GetBytes(piString);

        var piByteString = string.Join(" ",
            piBytes.Select(p => p.ToString("x")));

        WriteLine(piByteString);
        WriteLine(QuickMaths.PI);

        return Task.CompletedTask;
    }
}