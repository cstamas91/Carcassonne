using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class FieldAreaTests : BaseTest
    {
        private Tile Copy(Tile tile, Position newPosition, TileRotation rotation)
        {
            var newTile = new Tile(tile.Areas.Select(a => new SubArea(a.Edges, a.AreaType)).ToList(), newPosition);
            newTile.Rotation = rotation;
            return newTile;
        }

        private Tile GetBaseTile()
        {
            return new Tile(
                new List<SubArea>()
                {
                    new SubArea(new List<Direction>() {Direction.Up, Direction.Left}, AreaType.Road),
                    new SubArea(new List<Direction>() {Direction.UpLeft, Direction.LeftUp }, AreaType.Field),
                    new SubArea(new List<Direction>()
                    {
                        Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight, Direction.Down, Direction.DownLeft, Direction.LeftDown
                    }, AreaType.Field)
                }
                , new Position(0, 1));
        }

        [TestMethod]
        public void TestEvaluateFinished()
        {
            Tile baseTile = GetBaseTile();
            Tile t2 = Copy(baseTile, new Position(1, 1), TileRotation._90);
            Tile t3 = Copy(baseTile, new Position(1, 0), TileRotation._180);
            Tile t4 = Copy(baseTile, new Position(0, 0), TileRotation._270);

            FieldArea area = new FieldArea(baseTile[Direction.LeftUp]);
            area.AddSubArea(t2[Direction.LeftDown]);
            area.AddSubArea(t3[Direction.RightDown]);
            area.AddSubArea(t4[Direction.RightUp]);

            Assert.IsTrue(area.IsFinished);
        }
    }
}
