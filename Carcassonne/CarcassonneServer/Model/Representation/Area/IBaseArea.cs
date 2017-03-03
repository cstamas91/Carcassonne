using CarcassonneServer.Model.Representation.SubAreas;
using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.Area
{
    public interface IBaseArea
    {
        AreaType AreaType { get; }
        IEnumerable<ISubArea> SubAreas { get; }
        string GUID { get; }
        bool IsFinished { get; }
        int Score { get; }
        IEnumerable<Player> Owners { get; }
        HashSet<Position> Positions { get; }

        void AddSubArea(ISubArea subArea);
        void RemoveSubArea(ISubArea subArea);
        void AddMeeple(Meeple meeple, int id);
        bool IsAdjacent(Tile tile);
        bool CanAdd(ISubArea subArea);
        bool Contains(Position position);
        BaseArea Merge(IBaseArea other);
    }
}