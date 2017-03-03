using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.SubAreas
{
    public interface ISubArea
    {
        bool this[Direction direction] { get; }

        IEnumerable<Direction> ActualEdges { get; }
        AreaType AreaType { get; }
        List<Direction> Edges { get; }
        int Id { get; }
        Meeple Meeple { get; }
        Tile Parent { get; set; }
        int Score { get; }
        Position Position { get; }

        bool CanBeAdjacent(ISubArea other);
        bool IsAdjacent(ISubArea rhs);
        bool IsAdjacent(Tile rhs);
        void SetMeeple(Meeple meeple);
    }
}