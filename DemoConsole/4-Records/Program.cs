namespace DemoConsole._4_Records;

public static class Program
{
    // FEATURE: Record structs
    public record struct Person(string FirstName, string LastName);

    // FEATURE: Record structs, ability to add readonly for immutability
    public readonly record struct ReadonlyPerson(string FirstName, string LastName);

    public static void Main()
    {
        WriteLine();

        var albert = new Person("Albert", "Einstein");
        // Mutable by default!
        albert.FirstName = "Elsa";

        WriteLine(albert);

        ReadonlyPerson sylvester = new ReadonlyPerson("Isaac", "Newton") with
        {
            FirstName = "Sylvester",
            LastName = "Stallone"
        };

        WriteLine(sylvester);

        int x;
        var rectangle = new Rectangle(20, 10);
        // FEATURE: Mix Declarations and Variables in Deconstruction
        (x, int y) = rectangle;
        WriteLine(rectangle);
        WriteLine($"X -> {x}, Y -> {y}");
    }

    public record class Shape(int Width, int Height)
    {
        // FEATURE: Ability to mark ToString on base-record as sealed to prevent override
        public sealed override string ToString()
        {
            return $"Width = {Width}, Height = {Height}";
        }
    }

    public record Rectangle(int Width, int Height) : Shape(Width, Height)
    {
        // Compiler error
        // public override string ToString() => "nope";
    }
}