﻿using System;
using System.Collections.Generic;
using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class MarsMission
	{
		public IPlateau Plateau;
        private readonly int TeamLimit;
        private readonly int GamePointLimit;
        private readonly int CommandLimit;
        private List<Player> Players;

        public MarsMission(MissionConfig config)
		{
            GamePointLimit = config.GamePoints;
            TeamLimit = config.MaxTeamMembers;
            CommandLimit = config.InstructionLimit;

            Plateau = CreatePlatau(Coordinate.CreateCoordinate(config.MaxCoordinates.XCoordinate, config.MaxCoordinates.YCoordinate));

            Players = new List<Player>();
            SetupPlayers(config.PlayerCount);
        }
        
        public void SetupPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(new Player(Plateau, TeamLimit, CommandLimit, i+1));   
            }
            if (Players.Count == 0)   
            {
                Players.Add(new Player(Plateau, TeamLimit, CommandLimit, 1));
            }
        }

        public bool IsActive => Plateau.HasGamePoints();

        public IPlateau CreatePlatau(ICoordinate maxCoordinates)
        {
			return new Plateau(maxCoordinates, GamePointLimit);
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public void ActivateMission()
        {   
            Plateau.SetupGamePoints();
            Console.WriteLine("\nWelcome To Mars Mission!");
        }
    }
}