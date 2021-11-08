namespace Trit.DemoConsole._2_Interpolated_strings;

public static class Demo
{
    private const string FirstName = "Albert";
    private const string LastName = "Einstein";

    public static Task Main()
    {
        // FEATURE: Constant interpolated strings
        const string name = $"{FirstName} {LastName}";

        WriteLine($"const:\t'{name}'");

        WithBuffer();

        return Task.CompletedTask;
    }










    public static void WithBuffer()
    {
        Span<char> buffer = stackalloc char[64];
        string nameFromBuffer = InterpolatedStringHelpers.Create(
            provider: null,
            initialBuffer: buffer,
            handler: $"{FirstName} {LastName}");

        WriteLine($"with helper:\t'{nameFromBuffer}'");
        WriteLine($"buffer:\t'{new string(buffer)}' (x{buffer.Length})");
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