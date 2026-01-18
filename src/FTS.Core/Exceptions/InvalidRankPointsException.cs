namespace FTS.Core.Exceptions;

internal class InvalidRankPointsException : CustomException
{
    public int RankPoints { get; }

    public InvalidRankPointsException(int rankPoints) : base($"Invalid name: {rankPoints}. Must be >= 0.")
    {
        RankPoints = rankPoints;
    }
}
