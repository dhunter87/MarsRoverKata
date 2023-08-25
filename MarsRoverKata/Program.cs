using MarsRover.CLI;
using MarsRover.Helpers;
using MarsRover.Models;

class Program
{   
    static void Main()
    {
        var missionConfig = MissionSetup.CreateMissionConfig();
        var mission = new MarsMission(missionConfig);

        var players = mission.GetPlayers();
        InputValidator.SetupTeamRovers(players, mission.Plateau);

        mission.ActivateMission();

        while (mission.IsActive)
        {
            foreach (var player in players)
            {
                if (!mission.IsActive)
                {
                    continue;
                }

                TakePlayerTurn(mission, player);
            }
        }

        Console.WriteLine("Mission Over");
        PrintPlayerScores(players);
        ConsoleApp.PrintPlateauGrid(mission);
        Console.ReadLine();
    }

    private static void TakePlayerTurn(MarsMission mission, Player player)
    {
        Console.WriteLine("");
        ConsoleApp.PrintPlateauGrid(mission);

        player.GiveRoverInstructions(InputValidator.SetupRoverInstructions());        
    }

    private static void PrintPlayerScores(List<Player> players)
    {
        // calculate winner & output. concider a draw.
        var winningScore = 0;
        var winner = "";
        foreach (var player in players)
        {
            var score = player.GetScore();
            if (winningScore == 0 || winningScore < score)
            {
                winningScore = score;
                winner = player.Id;
            }
        }

        Console.WriteLine($"Winner: {winner}, Score: {winningScore}");
    }
}
