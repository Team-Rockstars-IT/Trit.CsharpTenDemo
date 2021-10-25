namespace DemoConsole._9_Generic_attributes;

public static class Program
{
    // FEATURE: Allow Generic Attributes (only available when langversion == preview)
    public class CompareWithAttribute<TComparer, TComparable> : Attribute
        where TComparer : IComparer<TComparable>
    {
    }

    public class PersonComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;

            return x.Age.CompareTo(y.Age);
        }
    }

    [CompareWith<PersonComparer,Person>]
    public record Person(string FirstName, string LastName, int Age);

    public static void Main()
    {
        // This retrieves the TComparer generic type parameter from CompareWithAttribute
        // and thus hopefully ending up with PersonComparer
        Type? comparerType = typeof(Person)
            .GetCustomAttributes()
            .Select(a => a.GetType())
            .FirstOrDefault(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(CompareWithAttribute<,>))
            ?.GenericTypeArguments[0];

        var comparer = (IComparer<Person>)(Activator.CreateInstance(
            comparerType
            ?? throw new InvalidOperationException("Oops")))!;

        WriteLine(comparer.Compare(new Person("Jane", "Doe", 42), new Person("Foo", "Bar", 42)));
    }
}
