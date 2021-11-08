namespace Trit.DemoConsole._5_Structs;

public static class Demo
{
    public static Task Main()
    {
        WriteLine(new Point());

        // FEATURE: "with" support for all structs
        WriteLine(new Point { X = 3 } with { X = 42 });

        // FEATURE: "with" support for anonymous types
        WriteLine(new { a = 1, b = 3.0f } with { a = 3 });

        return Task.CompletedTask;
    }
}

public struct Point
{
    // FEATURE: Field initializers!
    private readonly int _z = 3;

    // FEATURE: Ability to add no-arg constructor to structs
    public Point()
    {
        X = 1;
    }

    // FEATURE: Property initializers!
    public int X { get; set; } = 2;

    public int Y { get; set; } = 2;

    public override string ToString() =>
        $"X = {X}, Y = {Y}, Z = {_z}";
}