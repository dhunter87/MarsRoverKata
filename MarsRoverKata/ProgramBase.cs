using MarsRover.CLI;
using MarsRover.Helpers;
using MarsRover.Models;

internal class ProgramBase
{
    public static void TakePlayerTurn(MarsMission mission, Player player)
    {
        Console.WriteLine("");
        ConsoleApp.PrintPlateauGrid(mission);

        player.GiveRoverInstructions(InputValidator.SetupRoverInstructions(player));
    }

    public static void PrintGameResult(List<Player> players)
    {
        var finalPositions = new Dictionary<Player, int>();

        foreach (var player in players)
        {
            var score = player.GetScore();
            finalPositions.Add(player, score);
        }

        var sortedPositions = finalPositions.OrderByDescending(x => x.Value);

        var isDraw = CheckForDrawCondition(sortedPositions);

        if (!isDraw)
        {
            CheckForWinCondition(sortedPositions);
        }
    }

    private static bool CheckForDrawCondition(IOrderedEnumerable<KeyValuePair<Player, int>> scoreBoardPositions)
    {
        var topScore = scoreBoardPositions.ElementAt(0).Value;
        var drawPlayers = scoreBoardPositions.Where(kv => kv.Value == topScore).ToList();

        if (drawPlayers.Count > 1)
        {
            Console.WriteLine("Draw:");
            foreach (var player in drawPlayers)
            {
                PrintPlayerFinalPosition(ConvertToPositionString(1), player);
            }

            var nextPosition = 2;
            for (int i = drawPlayers.Count; i < scoreBoardPositions.Count(); i++)
            {
                PrintPlayerFinalPosition(ConvertToPositionString(nextPosition), scoreBoardPositions.ElementAt(i));
                nextPosition++;
            }

            return true;
        }

        return false;
    }

    private static void CheckForWinCondition(IOrderedEnumerable<KeyValuePair<Player, int>> scoreBoardPositions)
    {
        var nextPosition = 1;
        foreach (var position in scoreBoardPositions)
        {
            var stringPosition = ConvertToPositionString(nextPosition);

            if (stringPosition == "1st")
            {
                Console.Write($"WINNER: ");
            }

            PrintPlayerFinalPosition(stringPosition, position);
            nextPosition++;
        }
    }

    private static void PrintPlayerFinalPosition(string stringPosition, KeyValuePair<Player, int> position)
    {
        Console.WriteLine($"Player : {position.Key.Id}, Position: {stringPosition}, Points: {position.Value}");
    }

    private static string ConvertToPositionString(int position)
    {
        return position switch
        {
            1 => $"{position}st",
            2 => $"{position}nd",
            3 => $"{position}rd",
            4 or 5 or 6 or 7 or 8 or 9 or 10 => $"{position}th",
            _ => "",
        };
    }
}