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

        MissionSetup.SetupTeamRovers(players, mission);

        while (mission.IsActive)
        {
            foreach (var player in players)
            {
                foreach (var rover in player.Team)
                {
                    PrintCurrentPosition(rover);
                    var instructions = MissionSetup.SetupRoverInstructions();
                    player.GiveRoverInstructions(rover, instructions);
                }
            }

        }

        Console.WriteLine("Mission Over");
        PrintPlayeScores(players);
        Console.ReadLine();
    }

    private static void PrintPlayeScores(List<Player> players)
    {
        foreach (var player in players)
        {
            foreach (var rover in player.Team)
            {
                player.GetScore();
            }
        }
    }

    private static void PrintCurrentPosition(IRover rover)
    {
        Console.WriteLine("\n Current Rover position: \n");
        Console.WriteLine($"RoverId: {rover.GetId()}:");
        Console.WriteLine($"XCoordinate: {rover.Position.XCoordinate}, YCoordinate: {rover.Position.YCoordinate}, Bearing: {rover.Position.Bearing}");
    }
}
