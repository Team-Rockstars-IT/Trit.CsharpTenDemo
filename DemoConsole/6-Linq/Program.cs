namespace DemoConsole._6_Linq;

public static class Program
{
    public static void Main()
    {
        WriteLine();
        List<int> integers = new() { 1, 2, (int)QuickMaths.PI, 4 };
        var chunked = integers
            // FEATURE: Addition of Chunk to Linq method set
            .Chunk(size: 2)
            .ToArray();

        WriteLine($"First chunk: {string.Join(",", chunked[0])}");
        WriteLine($"second chunk: {string.Join(",", chunked[0])}");

        // FEATURE: TryGetNonEnumeratedCount which only returns count if an enumerator isn't needed to do so
        integers.TryGetNonEnumeratedCount(out int integerCount);
        WriteLine($"NonEnumeratedCount: {integerCount}");

        WriteLine($"Can get count for ordered enumerable? {integers.OrderBy(i => i).TryGetNonEnumeratedCount(out int _)}");
        WriteLine($"Can get count for filtered enumerable? {integers.Where(i => i % 2 == 0).TryGetNonEnumeratedCount(out int _)}");

        var people = new List<Person>
        {
            new("Albert", "Einstein", Age: 76),
            new("Steven", "Hawking", Age: 76),
            new("Isaac", "Newton", Age: 84),
        };
        // FEATURE: Addition of *By methods to Linq method set
        WriteLine($"Min by age: {people.MinBy(p => p.Age)?.FirstName}");
        WriteLine($"Max by age: {people.MaxBy(p => p.Age)?.FirstName}");
        WriteLine($"Distinct by age: {people.DistinctBy(p => p.Age)?.Count()}");
        WriteLine($"Except age 76: {people.ExceptBy(new[] { 76 }, p => p.Age).Count()}");
        WriteLine($"Union with Stallone: {people.UnionBy(new[] { new Person("Sylvester", "Stallone", Age: 75) }, p => p.Age).Count()}");
        WriteLine($"Intersect with age 76: {people.IntersectBy(new[] { 76 }, p => p.Age).Count()}");
    }
}

public record Person(string FirstName, string LastName, int Age);