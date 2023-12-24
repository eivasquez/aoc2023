public class Game
{
    public int GameId {get; set;}
    public Dictionary<BlockColor, int> Counts {get; set;} = new Dictionary<BlockColor, int>();
    public bool Possible {get;}

    public Game(int redCount, int greenCount, int blueCount, string input)
    {
        Possible = true;
        var blockCounts = new Dictionary<BlockColor, int>
        {
            {BlockColor.Red, redCount},
            {BlockColor.Green, greenCount},
            {BlockColor.Blue, blueCount}
        };

        var parts = input.Split(":").Select(s => s.Trim());
        GameId = int.Parse(parts.ElementAt(0).Split(" ").ElementAt(1));

        var rounds = parts.ElementAt(1).Split(";").Select(s => s.Trim());
        foreach (var round in rounds)
        {
            var blocks = round.Split(",").Select(s => s.Trim());
            foreach (var block in blocks)
            {
                var blockParts = block.Split(" ").Select(s => s.Trim());
                var count = int.Parse(blockParts.ElementAt(0));
                var color = Enum.Parse<BlockColor>(blockParts.ElementAt(1), true);
                if (!Counts.TryAdd(color, count))
                    Counts[color] = count > Counts[color] ? count : Counts[color];

                if (blockCounts[color] < count)
                    Possible = false;
            }
        }
    }

    public int Power()
    {
        return Counts.Values.Aggregate((a, b) => a * b);
    }
}