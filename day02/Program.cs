internal class Program
{
    private static void Main(string[] args)
    {
        var sum = 0;

        foreach (var line in File.ReadLines(@"./input.txt"))
        {
            var game = new Game(12, 13, 14, line);
            if (game.Possible)
                sum += game.GameId;
        }

        Console.WriteLine(sum);
    }
}