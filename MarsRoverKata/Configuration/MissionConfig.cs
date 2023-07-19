namespace MarsRover.Helpers
{
    public class MissionConfig
    {
        public readonly (int,int) MaxCoordinates;
        public readonly int GamePoints;
        public readonly int MaxTeamMembers;
        public readonly int InstructionLimit;
        public readonly int PlayerCount;

        public MissionConfig((int,int) maxCoordinates, int gamePoints, int maxTeamMembers, int instructionLimit, int playerCount)
        {
            MaxCoordinates = maxCoordinates;
            GamePoints = gamePoints;
            MaxTeamMembers = maxTeamMembers;
            InstructionLimit = instructionLimit;
            PlayerCount = playerCount;
        }
    }
}