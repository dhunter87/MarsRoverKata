using MarsRover.Helpers;
using MarsRover.Interfaces;
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
                TakePlayerTurn(mission, player);
            }
        }

        Console.WriteLine("Mission Over");
        PrintPlayerScores(players);
        Console.ReadLine();
    }

    private static void TakePlayerTurn(MarsMission mission, Player player)
    {
        foreach (var rover in player.Team)
        {
            PrintCurrentPosition(player, rover);
            var instructions = InputValidator.SetupRoverInstructions();
            player.GiveRoverInstructions(rover, instructions);
        }
    }

    private static void PrintPlayerScores(List<Player> players)
    {
        foreach (var player in players)
        {
            player.GetScore();
        }
    }

    private static void PrintCurrentPosition(Player player, IRover rover)
    {
        player.GetScore();
        Console.WriteLine("\n Current Rover position: \n");
        Console.WriteLine($"RoverId: {rover.GetId()}:");
        Console.WriteLine($"XCoordinate: {rover.Position.XCoordinate}, YCoordinate: {rover.Position.YCoordinate}, Bearing: {rover.Position.Bearing}");
    }
}
