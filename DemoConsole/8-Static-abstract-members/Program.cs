#pragma warning disable CA2252

namespace Trit.DemoConsole._8_Static_abstract_members;

public static class Demo
{
    public interface INumber<T>
        where T : INumber<T>
    {
        // FEATURE: Static abstract members in interfaces
        //          (only available when EnablePreviewFeatures == true
        //                and LangVersion == Preview)
        static abstract T operator +(T left, T right);
    }

    public record SimpleInt32 : INumber<SimpleInt32>
    {
        private readonly int _value;

        private SimpleInt32(int value)
        {
            _value = value;
        }

        public static SimpleInt32 operator +(SimpleInt32 left, SimpleInt32 right)
        {
            WriteLine($"Adding {left._value} to {right._value} = ");
            return left._value + right._value;
        }

        public static implicit operator int(SimpleInt32 simpleInt32)
            => simpleInt32._value;

        public static implicit operator SimpleInt32(int integer)
            => new(integer);

        public override string ToString() => _value.ToString();
    }

    public static Task Main()
    {
        SimpleInt32 left = 42;
        SimpleInt32 right = 43;

        Calculation(left, right);

        return Task.CompletedTask;
    }

    public static void Calculation<T>(T left, T right)
        where T : INumber<T>
    {
        WriteLine(left + right);
    }
}