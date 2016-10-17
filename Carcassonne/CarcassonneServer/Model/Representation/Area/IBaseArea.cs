using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.Area
{
    public interface IBaseArea
    {
        AreaType AreaType { get; }
        IEnumerable<SubArea> SubAreas { get; }
        string GUID { get; }
        bool IsFinished { get; }
        short Score { get; }

        void AddSubArea(SubArea subArea);
        void AddMeeple(Meeple meeple, SubArea subArea);
        BaseArea Merge(BaseArea other);
        ConnectingPoint NeighborDirection(Position other);
    }
}