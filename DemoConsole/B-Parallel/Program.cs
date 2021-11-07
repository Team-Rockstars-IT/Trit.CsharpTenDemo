namespace Trit.DemoConsole.B_Parallel;

public static class Demo
{
    public static async Task Main()
    {
        using var httpClient = new HttpClient();
        (string value, int index)[] linksToGet =
            await GetEventLinksFromTeamUpIt(httpClient);
        var organisers = new string[linksToGet.Length];

        // FEATURE: Async version of Parallel.ForEach()
        await Parallel.ForEachAsync(
            linksToGet,
            async (eventLink, cancellationToken) =>
            {
                HttpResponseMessage linkResponse = await httpClient
                    .GetAsync(eventLink.value, cancellationToken);

                string eventPageContent = await linkResponse
                    .Content
                    .ReadAsStringAsync(cancellationToken);

                organisers[eventLink.index] =
                    GetOrganiser(eventPageContent);
            });

        WriteLine(
            "Organisers with an active event on TeamUp IT: " +
            string.Join(", ",
                organisers.Distinct().OrderBy(n => n)));
    }

    #region Boilerplate

    private static async Task<(string value, int index)[]> GetEventLinksFromTeamUpIt(HttpClient httpClient)
    {
        string eventsHomepageContent = await (await httpClient.GetAsync("https://www.teamupit.nl/events/")).Content.ReadAsStringAsync();
        return Regex.Matches(eventsHomepageContent, "\"(https://www.teamupit.nl/event/[^/\"]+?/)\"")
            .Select((match, index) => (match.Groups[1].Value, index))
            .ToArray();
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

    #endregion
}