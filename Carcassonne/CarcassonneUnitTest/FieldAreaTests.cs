using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class FieldAreaTests : BaseTest
    {
        private Tile Copy(Tile tile, Position newPosition, TileRotation rotation)
        {
            //var newTile = new Tile(tile.Areas.Select(a => BaseSubArea.Get(a.Edges, a.AreaType)).ToList(), newPosition);
            //newTile.Rotation = rotation;
            //return newTile;
            return null;
        }

        private Tile GetBaseTile()
        {
            //return new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(new List<Direction>() {Direction.Up, Direction.Left}, AreaType.Road),
            //        BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.LeftUp }, AreaType.Field),
            //        BaseSubArea.Get(new List<Direction>()
            //        {
            //            Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight, Direction.Down, Direction.DownLeft, Direction.LeftDown
            //        }, AreaType.Field)
            //    }
            //    , new Position(0, 1));
            return null;
        }

        [TestMethod]
        public void TestEvaluateFinished()
        {
            //Tile baseTile = GetBaseTile();
            //Tile t2 = Copy(baseTile, new Position(1, 1), TileRotation._90);
            //Tile t3 = Copy(baseTile, new Position(1, 0), TileRotation._180);
            //Tile t4 = Copy(baseTile, new Position(0, 0), TileRotation._270);

            //FieldArea area = BaseArea.Get(baseTile[Direction.LeftUp]) as FieldArea;
            //area.AddSubArea(t2[Direction.LeftDown]);
            //area.AddSubArea(t3[Direction.RightDown]);
            //area.AddSubArea(t4[Direction.RightUp]);

            //Assert.IsTrue(area.IsFinished);
            Assert.Fail();
        }
    }
}
