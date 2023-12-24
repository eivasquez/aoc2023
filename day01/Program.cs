internal static class Program
{
    static void Part1()
    {
        CalculateCalibrationValues(false);
    }

    static void Part2()
    {
        CalculateCalibrationValues(true);
    }

    static void CalculateCalibrationValues(bool digitize)
    {
        var sum = 0;

        foreach (var line in File.ReadLines(@"./input.txt"))
        {
            var digits = digitize ? GetDigitCharsWithWords(line) : GetDigitChars(line);
            var number = int.Parse($"{digits.First()}{digits.Last()}");
            sum += number;
        }

        Console.WriteLine(sum);
    }

    static IEnumerable<char> GetDigitChars(string input)
    {
        return input.Where(c => char.IsDigit(c));
    }

    static IEnumerable<char> GetDigitCharsWithWords(string input)
    {
        var digitMap = new Dictionary<string, char> {
            {"one", '1'},
            {"two", '2'},
            {"three", '3'},
            {"four", '4'},
            {"five", '5'},
            {"six", '6'},
            {"seven", '7'},
            {"eight", '8'},
            {"nine", '9'}
        };

        var orderedLocations = new SortedDictionary<int, char>();
        foreach (var digit in digitMap)
        {
            if (input.Contains(digit.Key))
            {
                var indices = input.IndicesOf(digit.Key);
                indices.ToList().ForEach(i => orderedLocations.Add(i, digit.Value));                
            }
        }

        for (int i = 0; i < input.Length; ++i)
        {
            if (char.IsDigit(input[i]))
                orderedLocations.Add(i, input[i]);
        }

        var firstReplacement = orderedLocations.FirstOrDefault();
        var lastReplacement = orderedLocations.LastOrDefault();

        return new char[] {firstReplacement.Value, lastReplacement.Value};
    }

    static IEnumerable<int> IndicesOf(this string target, string search)
    {
        int location = target.IndexOf(search);
        while (location != -1)
        {
            yield return location;
            location = target.IndexOf(search, location + 1);
        }
    }

    private static void Main(string[] args)
    {
        Part1();
        Part2();
    }
}