namespace MarsRover.Helpers
{
    public class MissionConfig
    {
        public readonly (int,int) MaxCoordinates;
        public readonly int MaxTeamMembers;
        public readonly int InstructionLimit;
        public readonly int PlayerCount;

        public MissionConfig((int,int) maxCoordinates, int maxTeamMembers, int instructionLimit, int playerCount)
        {
            MaxCoordinates = maxCoordinates;
            MaxTeamMembers = maxTeamMembers;
            InstructionLimit = instructionLimit;
            PlayerCount = playerCount;
        }
    }
}