namespace Trit.DemoConsole.F_PriorityQueue;

public static class Demo
{
    public static Task Main()
    {
        // FEATURE: PriorityQueue
        var sandwichPriority = new PriorityQueue<string, int>();

        sandwichPriority.Enqueue("bread", priority: 0);
        sandwichPriority.Enqueue("ham", priority: 3);
        sandwichPriority.Enqueue("butter", priority: 1);
        sandwichPriority.Enqueue("bread", priority: 4);
        sandwichPriority.Enqueue("cheese", priority: 2);

        while (sandwichPriority
               .TryDequeue(out string? part, out int priority))
        {
            WriteLine($"{part} @ {priority}");
        }

        return Task.CompletedTask;
    }
}