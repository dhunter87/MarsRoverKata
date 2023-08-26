using MarsRover.Helpers;
using MarsRover.Models;

class Program : ProgramBase
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
        PrintGameResult(players);
        Console.ReadLine();
    }
}
