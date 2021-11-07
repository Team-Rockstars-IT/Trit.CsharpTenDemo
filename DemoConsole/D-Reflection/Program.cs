namespace Trit.DemoConsole.D_Reflection;

public static class Demo
{
    public static Task Main()
    {
        WriteLine("FirstName: " +
                  $"{GetNullability(nameof(Person.FirstName))}");
        WriteLine("MiddleName: " +
                  $"{GetNullability(nameof(Person.MiddleName))}");
        WriteLine("LastName: " +
                  $"{GetNullability(nameof(Person.LastName))}");
        WriteLine("Age: " +
                  $"{GetNullability(nameof(Person.Age))}");
        WriteLine("HasDriversLicense: " +
                  $"{GetNullability(nameof(Person.HasDriversLicense))}");

        return Task.CompletedTask;
    }

    private static NullabilityState GetNullability(string propertyName)
    {
        NullabilityInfoContext context = new();
        return context
            // FEATURE: A way to determine a property's
            //          nullability via reflection
            .Create(typeof(Person).GetProperty(propertyName)!)
            .ReadState;
    }

    public record Person(
        string FirstName,
        string? MiddleName,
        string LastName,
        int Age,
        bool? HasDriversLicense);
}