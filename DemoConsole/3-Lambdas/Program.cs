namespace DemoConsole._3_Lambdas;

public static class Program
{
    public static void Main()
    {
        WriteLine();

        // FEATURE: Lambda assigned to var
        var getHashCode = 1.GetHashCode;
        var getOne = () => 1;

        // FEATURE: Explicit lambda return type
        var addOne = void (ref int x) => x++;

        int counter = 0; addOne(ref counter);

        WriteLine(counter + 1);

        // FEATURE: Attributes on lambda
        var onEntireLambda = [Obsolete] (int a, int b) => a + b;
        var onParameter = ([Required] int a, int? b) => a + b;
    }
}