namespace Trit.DemoConsole._8_Generic_attributes;

public static class Demo
{
    // FEATURE: Allow Generic Attributes
    //          (only available when langversion == preview)
    public class CompareWithAttribute<TComparer, TComparable>
        : Attribute
        where TComparer : IComparer<TComparable>
    {
    }

    [CompareWith<PersonComparer, Person>]
    public record Person(string FirstName, string LastName, int Age);

    public static Task Main()
    {
        IComparer<Person> comparer = GetComparerFromAttribute();

        var comparisonResult = comparer.Compare(
            new Person("Jane", "Doe", 42),
            new Person("Foo", "Bar", 42));

        WriteLine("Result when two people are compared " +
                  $"by age: {comparisonResult}");

        return Task.CompletedTask;
    }

    #region Boilerplate

    private static IComparer<Person> GetComparerFromAttribute()
    {
        // This retrieves the TComparer generic type parameter from CompareWithAttribute
        // and thus hopefully ending up with PersonComparer
        Type? comparerType = typeof(Person)
            .GetCustomAttributes()
            .Select(a => a.GetType())
            .FirstOrDefault(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(CompareWithAttribute<,>))
            ?.GenericTypeArguments[0];

        return (IComparer<Person>)(Activator.CreateInstance(
            comparerType
            ?? throw new InvalidOperationException("Oops")))!;
    }

    private class PersonComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;

            return x.Age.CompareTo(y.Age);
        }
    }

    #endregion
}