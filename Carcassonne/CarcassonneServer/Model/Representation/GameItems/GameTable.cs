using System.Collections.Generic;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;
using CarcassonneServer.Model.Representation.SubAreas;
using System;

namespace CarcassonneServer.Model.Representation.GameItems
{
    public class GameTable
    {
        private List<IBaseArea> areas;
        public IEnumerable<IBaseArea> Areas => areas;
        private int xDimension;
        private int yDimension;
        public int X => xDimension;
        public int Y => yDimension;

        public GameTable()
        {
            InitializeTable();
        }        

        public void SetTile(Tile tile)
        {
            List<IBaseArea> adjacentAreas = GetAdjacentAreas(tile).ToList();

            foreach (ISubArea subArea in tile.Areas)
            {
                var areasToAdd = adjacentAreas.Where(area => area.CanAdd(subArea)).ToList();
                if (areasToAdd.Count == 0)
                    areas.Add(BaseArea.Get(subArea));

                if (areasToAdd.Count == 1)
                    areasToAdd[0].AddSubArea(subArea);

                if (areasToAdd.Count > 1)
                    Merge(subArea, areasToAdd.ToList());
            }
        }

        public void SetMeeple(Meeple meeple, Position position, int id)
        {
            IBaseArea targetArea = areas.FirstOrDefault(area => area.Contains(position) && area.SubAreas.Any(subArea => subArea.Id == id));
            if (targetArea != null)
                targetArea.AddMeeple(meeple, id);
        }

        #region Private helpers
        private IEnumerable<IBaseArea> GetAdjacentAreas(Tile tile) => areas.Where(a => a.IsAdjacent(tile));

        private void Merge(ISubArea subArea, List<IBaseArea> areasToAdd)
        {
            IBaseArea fst = areasToAdd[0];
            fst.AddSubArea(subArea);
            for (int i = 1; i < areasToAdd.Count; i++)
            {
                try
                {
                    fst.Merge(areasToAdd[i]);
                }
                catch 
                {
                    continue;
                }
            }
        }

        private void InitializeTable()
        {
            //xDimension = 21;
            //yDimension = 19;
            //areas = new List<IBaseArea>();
            //SetTile(new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.UpRight, Direction.Up }, AreaType.Castle),
            //        BaseSubArea.Get(new List<Direction>() {Direction.RightUp, Direction.LeftUp }, AreaType.Field),
            //        BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left }, AreaType.Road),
            //        BaseSubArea.Get(new List<Direction>() {Direction.RightDown, Direction.DownRight, Direction.Down, Direction.DownLeft, Direction.LeftDown }, AreaType.Field)
            //    }, 
            //    new Position((xDimension / 2), (yDimension / 2))));
            throw new NotImplementedException();
        }
        #endregion Private helpers
    }
}
