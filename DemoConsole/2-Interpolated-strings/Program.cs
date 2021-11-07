namespace Trit.DemoConsole._2_Interpolated_strings;

public static class Demo
{
    public static Task Main()
    {
        const string firstName = "Albert";
        const string lastName = "Einstein";

        // FEATURE: Constant interpolated strings
        const string name = $"{firstName} {lastName}";

        WriteLine($"const:\t'{name}'");

        Span<char> buffer = stackalloc char[64];
        string nameFromBuffer = InterpolatedStringHelpers.Create(
            provider: null,
            initialBuffer: buffer,
            handler: $"{firstName} {lastName}");

        WriteLine($"with helper:\t'{nameFromBuffer}'");
        WriteLine($"buffer:\t'{new string(buffer)}'");

        return Task.CompletedTask;
    }

    public static class InterpolatedStringHelpers
    {
        // Note: this method is available as string.Create,
        //       it's duplicated here for illustration purposes
        public static string Create(
            IFormatProvider? provider,
            Span<char> initialBuffer,
            // FEATURE: DefaultInterpolatedStringHandler is new
            //          and allows lowered interpolated string code
            //          to be more efficient
            [InterpolatedStringHandlerArgument(
                "provider", "initialBuffer"
            )]
            ref DefaultInterpolatedStringHandler handler)
        {
            return handler.ToStringAndClear();
        }
    }
}