using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.Construction
{
    public interface IBaseConstruction
    {
        TileSideType AreaType { get; }
        IEnumerable<Tile> EdgeTiles { get; }
        string GUID { get; }
        bool IsFinished { get; }
        short Score { get; }
        int Size { get; }

        void AddElement(ref Tile element);
        void AddMeeple(Meeple meeple);
        BaseConstruction Merge(BaseConstruction other);
        Direction NeighborDirection(Position other);
    }
}