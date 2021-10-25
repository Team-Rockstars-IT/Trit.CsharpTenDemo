// FEATURE: File scoped namespaces
namespace DemoConsole._1_Namespaces_and_usings;

public static class Program
{
    public static void Main()
    {
        WriteLine();

        var piString = QuickMaths.PI.ToString(CultureInfo.InvariantCulture);

        var piBytes = Encoding.ASCII.GetBytes(piString);

        var piByteString = string.Join(" ", piBytes.Select(p => p.ToString("x")));

        WriteLine(piByteString);
        WriteLine(QuickMaths.PI);
    }
}