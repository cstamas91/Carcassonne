using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.Area
{
    public interface IBaseArea
    {
        AreaType AreaType { get; }
        IEnumerable<SubArea> SubAreas { get; }
        string GUID { get; }
        bool IsFinished { get; }
        int Score { get; }
        IEnumerable<Player> Owners { get; }

        void AddSubArea(SubArea subArea);
        void RemoveSubArea(SubArea subArea);
        void AddMeeple(Meeple meeple, SubArea subArea);
        BaseArea Merge(BaseArea other);
    }
}