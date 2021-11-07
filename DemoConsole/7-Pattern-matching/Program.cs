namespace Trit.DemoConsole._7_Pattern_matching;

public static class Demo
{
    public static Task Main()
    {
        var exPresident = new Person(
            "Donald", "Trump",
            new Face(Color: "Orange"));

        WriteLine(exPresident switch
        {
            // FEATURE: Extended property patterns
            { Face.Color: "Orange" } => "It's Trump!",
            // OLD: { Face: { Color: "Orange" } } => "It's Trump!",
            _ => "Most likely not Trump"
        });

        return Task.CompletedTask;
    }
}

public record Person(string FirstName, string LastName, Face Face);

public record Face(string Color);