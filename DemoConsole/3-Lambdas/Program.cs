namespace Trit.DemoConsole._3_Lambdas;

public static class Demo
{
    public static Task Main()
    {
        // FEATURE: Lambda assigned to var
        var getHashCode = 1.GetHashCode;
        var getOne = () => 1;

        // FEATURE: Explicit lambda return type
        var addOne = void (ref int x) => x++;

        int counter = 0;
        WriteLine($"Counter is {counter}" +
                  "\nbefore calling void lambda\nwith ref parameter");

        addOne(ref counter);

        WriteLine($"\nCounter is {counter}" +
                  "\nafter calling void lambda\nwith ref parameter");

        // FEATURE: Attributes on lambda
        var onEntireLambda = [Obsolete] (int a, int b) => a + b;
        var onParameter = ([Required] int a, int? b) => a + b;

        return Task.CompletedTask;
    }
}