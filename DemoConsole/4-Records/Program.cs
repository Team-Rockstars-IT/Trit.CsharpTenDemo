namespace Trit.DemoConsole._4_Records;

public static class Demo
{
    // FEATURE: Record structs
    public record struct Person(
        string FirstName, string LastName
    );

    // FEATURE: Ability to add readonly for immutability
    public readonly record struct ReadonlyPerson(
        string FirstName, string LastName
    );

    public static Task Main()
    {
        var albert = new Person("Albert", "Einstein");
        // Mutable by default!
        albert.FirstName = "Elsa";

        WriteLine(albert);

        ReadonlyPerson sylvester =
            new ReadonlyPerson("Isaac", "Newton")
                with
                {
                    FirstName = "Sylvester",
                    LastName = "Stallone"
                };

        WriteLine(sylvester);

        int x;
        var rectangle = new Rectangle(Width: 20, Height: 10);
        // FEATURE: Mix Declarations
        //          and Variables in Deconstruction
        (x, int y) = rectangle;
        WriteLine(rectangle);
        WriteLine($"X -> {x}, Y -> {y}");

        return Task.CompletedTask;
    }

    public record class Shape(int Width, int Height)
    {
        // FEATURE: Ability to mark ToString on base-record
        //          as sealed to prevent override
        public sealed override string ToString()
        {
            return $"Width = {Width}, Height = {Height}";
        }
    }

    public record Rectangle(int Width, int Height)
        : Shape(Width, Height)
    {
        // Compiler error
        // public override string ToString() => "nope";
    }
}