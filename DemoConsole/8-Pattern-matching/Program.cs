namespace DemoConsole._8_Pattern_matching;

public static class Program
{
    public static void Main()
    {
        WriteLine();

        var exPresident = new Person("Donald", "Trump", new Face(Color: "Orange"));

        WriteLine(exPresident switch
        {
            // FEATURE: Extended property patterns
            { Face.Color: "Orange" } => "It's Trump!",
            // OLD: { Face: { Color: "Orange" } } => "It's Trump!",
            _ => "Most likely not Trump"
        });
    }
}

public record Person(string FirstName, string LastName, Face Face);

public record Face(string Color);