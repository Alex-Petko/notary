namespace Shared.Tests;

public sealed class RandomString
{
    private readonly int _min;
    private readonly int _max;

    public RandomString(int min, int max)
    {
        if (min < 0)
            throw new ArgumentException($"{nameof(min)} cannot be less than 0");

        if (max < min)
            throw new ArgumentException($"{nameof(min)} cannot be greater than {nameof(max)}");

        _min = min;
        _max = max;
    }

    public RandomString(int length) : this(length, length)
    {
    }

    public override string ToString()
    {
        var length = Random.Shared.Next(_min, _max + 1);

        if (length == 0)
            return string.Empty;

        var result = string.Concat(Enumerable.Range(0, length)
            .Select(x => (char)Random.Shared.Next(char.MinValue, char.MaxValue + 1)));

        return result!;
    }
}