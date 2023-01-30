namespace BeautifulNumbers;

public sealed class BeautifulNumbersCounter
{
    private readonly int _notationDigitsCount;
    private readonly IReadOnlyCollection<long> _startCombinations;

    private BeautifulNumbersCounter(int notationDigitsCount)
    {
        _notationDigitsCount = notationDigitsCount;

        var combinations = new List<long>();
        for (var i = 0; i < notationDigitsCount; i++)
        {
            combinations.Add(1);
        }

        _startCombinations = combinations;
    }

    public long GetCount(int numberLength)
    {
        if (numberLength < 2)
            throw new ArgumentOutOfRangeException(nameof(numberLength));

        var numbersCountToCompare = numberLength / 2;
        var hasDigitInTheMiddle = IsOdd(numberLength);

        var combinations = _startCombinations;

        for (var n = 1; n < numbersCountToCompare; n++)
        {
            combinations = GetNextCombinations(combinations);
        }

        var result = combinations.Aggregate(0L, (sum, current) => sum += current * current);

        return hasDigitInTheMiddle ? result * _notationDigitsCount : result;
    }

    private IReadOnlyCollection<long> GetNextCombinations(IReadOnlyCollection<long> combinations)
    {
        var newLength = combinations.Count + _notationDigitsCount - 1;
        var newCombinations = new List<long>(newLength);

        for (var k = 0; k < newLength; k++)
        {
            var currentResult = 0L;

            for (var i = 0; i < _notationDigitsCount; i++)
            {
                var toAdd = combinations.ElementAtOrDefault(k - i);
                currentResult += toAdd;
            }

            newCombinations.Add(currentResult);
        }

        return newCombinations;
    }

    public static BeautifulNumbersCounter Create(int notationDigitsCount)
    {
        if (notationDigitsCount < 2)
            throw new ArgumentOutOfRangeException(nameof(notationDigitsCount));

        return new(notationDigitsCount);
    }

    private static bool IsOdd(int number)
        => number % 2 != 0;
}

