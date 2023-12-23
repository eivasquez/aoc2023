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
        var lineToProcess = digitize ? DigitizeString(line) : line;

        var digits = lineToProcess.Where(c => char.IsDigit(c));
        var number = int.Parse($"{digits.First()}{digits.Last()}");
        sum += number;
    }

    Console.WriteLine(sum);
}

static string DigitizeString(string input)
{
    var digitMap = new Dictionary<string, string> {
        {"one", "1"},
        {"two", "2"},
        {"three", "3"},
        {"four", "4"},
        {"five", "5"},
        {"six", "6"},
        {"seven", "7"},
        {"eight", "8"},
        {"nine", "9"}
    };

    var orderedLocations = new SortedDictionary<int, string>();
    foreach (var digit in digitMap)
    {
        if (input.Contains(digit.Key))
            orderedLocations.Add(input.IndexOf(digit.Key), digit.Key);
    }

    for (int i = 0; i < input.Length; ++i)
    {
        if (char.IsDigit(input[i]))
            orderedLocations.Add(i, $"{input[i]}");
    }

    var firstReplacement = orderedLocations.FirstOrDefault();
    var lastReplacement = orderedLocations.LastOrDefault();

    if (digitMap.ContainsKey(firstReplacement.Value))
        input = input.Replace(firstReplacement.Value, digitMap[firstReplacement.Value]);

    if (digitMap.ContainsKey(lastReplacement.Value))
        input = input.Replace(lastReplacement.Value, digitMap[lastReplacement.Value]);

    return input;
}

Part1();
Part2();