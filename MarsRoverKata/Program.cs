using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;
using MarsRover.Models;

class Program
{   
    static void Main()
    {
        var missionConfig = MissionSetup.CreateMissionConfig();

        var mission = new MarsMission(missionConfig);

        var players = mission.GetConfiguredPlayers();

        mission.SetupTeamRovers();
        mission.StartMission();

        while (mission.IsActive)
        {
            foreach (var player in players)
            {
                foreach (var rover in player.Team)
                {
                    PrintCurrentPosition(player, rover);
                    var instructions = mission.SetupRoverInstructions();
                    player.GiveRoverInstructions(rover, instructions);
                }
            }
        }

        Console.WriteLine("Mission Over");
        PrintPlayerScores(players);
        Console.ReadLine();
    }

    private static void PrintPlayerScores(List<Player> players)
    {
        foreach (var player in players)
        {
            foreach (var rover in player.Team)
            {
                player.GetScore();
            }
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
