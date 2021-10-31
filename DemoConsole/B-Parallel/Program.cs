using System.Text.RegularExpressions;

namespace DemoConsole.B_Parallel;

public static class Program
{
    public static async Task Main()
    {
        WriteLine();

        var httpClient = new HttpClient();
        string eventsHomepageContent = await (await httpClient.GetAsync("https://www.teamupit.nl/events/")).Content.ReadAsStringAsync();
        (string value, int index)[] linksToGet = Regex.Matches(eventsHomepageContent, "\"(https://www.teamupit.nl/event/[^/\"]+?/)\"")
            .Select((match, index) => (match.Groups[1].Value, index))
            .ToArray();

        string[] organisers = new string[linksToGet.Length];

        // FEATURE: Async version of Parallel.ForEach()
        await Parallel.ForEachAsync(linksToGet, async (eventLink, cancellationToken) =>
        {
            string eventPageContent = await (await httpClient.GetAsync(eventLink.value, cancellationToken))
                .Content
                .ReadAsStringAsync(cancellationToken);

            organisers[eventLink.index] = GetOrganiser(eventPageContent);
        });

        WriteLine(string.Join(", ", organisers.Distinct().OrderBy(n => n)));
    }

    private static string GetOrganiser(string eventPageContent)
    {
        // Quick and dirty way of getting the text inside of the <span> inside the chief block
        int chiefIndex = eventPageContent.IndexOf("<div class=\"chief-box\">", StringComparison.Ordinal);
        int endChiefIndex = eventPageContent.IndexOf("</div>", chiefIndex, StringComparison.Ordinal);
        ReadOnlySpan<char> chiefBlock = eventPageContent.AsSpan().Slice(chiefIndex, endChiefIndex - chiefIndex);
        int spanStartIndex = chiefBlock.IndexOf("<span>");
        int spanEndIndex = chiefBlock.IndexOf("</span>");
        var nameStartIndex = spanStartIndex + "<span>".Length;

        return new string(chiefBlock.Slice(nameStartIndex, spanEndIndex - nameStartIndex));
    }
}
