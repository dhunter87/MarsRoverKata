﻿using MarsRover.Configuration;
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRover.Helpers
{
    public static class MissionSetup
	{
        public static MissionConfig CreateMissionConfig()
        {
            var difficultySetting = GetDifficultySetting();
            var maxCoordinates = SetupPlateauCoordinates(difficultySetting);
            var gamePoints = SetupGamepoints(difficultySetting);
            var maxTeamMembers = SetUpTeamLimits(difficultySetting);
            var instructionLimit = SetupInstructionLimit(difficultySetting);
            var playerCount = SetupPlayerCount();

            return new MissionConfig(maxCoordinates, gamePoints, maxTeamMembers, instructionLimit, playerCount);
        }

        private static DifficultySetting GetDifficultySetting()
        {
            DifficultySetting difficultySetting;
            while (true)
            {
                Console.WriteLine("Select Mission Difficulty. 1 = Easy, 2 = Medium, 3 = Hard!");
                var difficulty = Console.ReadLine();

                if (!int.TryParse(difficulty, out int difficultyLevel))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;
                }

                difficultySetting = (DifficultySetting)Enum.Parse(typeof(DifficultySetting), difficultyLevel.ToString());
                break;
            }
            return difficultySetting;
        }

        private static ICoordinate SetupPlateauCoordinates(DifficultySetting setting)
        {
            var maxCoordinateDictionary = new Dictionary<DifficultySetting, int> { { DifficultySetting.Easy, 5 }, { DifficultySetting.Medium, 10 }, { DifficultySetting.Hard, 15 } };

            if (!maxCoordinateDictionary.TryGetValue(setting, out int maxCoord))
            {
                Console.WriteLine($"The maximum coordinate for {setting} is: {maxCoord}");
            }

            return Coordinate.CreateCoordinate(maxCoord, maxCoord);
        }

        private static int SetupGamepoints(DifficultySetting setting)
        {
            return setting switch
            {
                DifficultySetting.Easy => 3,
                DifficultySetting.Medium => 5,
                DifficultySetting.Hard => 10,
                _ => 0,
            };
        }

        private static int SetUpTeamLimits(DifficultySetting setting)
        {
            return setting switch
            {
                DifficultySetting.Easy => 1,
                DifficultySetting.Medium => 2,
                DifficultySetting.Hard => 3,
                _ => 1,
            };
        }

        private static int SetupInstructionLimit(DifficultySetting setting)
        {
            return setting switch
            {
                DifficultySetting.Easy => 5,
                DifficultySetting.Medium => 10,
                DifficultySetting.Hard => 5,
                _ => 0,
            };
        }

        private static int SetupPlayerCount()
        {
            while (true)
            {
                Console.WriteLine("Enter Number Players Joining 'Mars Mission!' (Max 4 Players)");
                var playerCount = Console.ReadLine();

                if (!int.TryParse(playerCount, out int players))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;

                }
                if (players <= 0 || players > 4)
                {
                    Console.WriteLine("Invalid input - out of range.");
                    continue;
                }

                return players;
            }
        }
    }
}

