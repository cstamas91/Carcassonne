using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.GameItems;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class GameTableTests : BaseTest
    {
        /// <summary>
        /// GameTable should have four areas right after initialization.
        /// </summary>
        [TestMethod]
        public void TestInitialAreaCount()
        {
            GameTable table = new GameTable();
            Assert.IsTrue(table.Areas.Count() == 4);
        }

        /// <summary>
        /// Should be able to add a second tile to a freshly initialized gametable.
        /// Should have five areas.
        /// </summary>
        [TestMethod]
        public void TestSetTileCount()
        {
            GameTable table = new GameTable();
            Tile tile = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.Up, Direction.UpRight }, AreaType.Castle),
                    BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left, Direction.Down, Direction.DownLeft, Direction.DownRight, Direction.LeftUp, Direction.LeftDown, Direction.RightUp, Direction.RightDown }, AreaType.Field)
                },
                new Position(11, 9))
            { Rotation = TileRotation._180 };
            table.SetTile(tile);

            Assert.IsTrue(table.Areas.Count() == 5);
        }
        /// <summary>
        /// Should be able to add a second tile to a freshly initialized gametable.
        /// Should have a castle area finished with two subareas.
        /// </summary>
        [TestMethod]
        public void TestSetTileFinishedState()
        {
            GameTable table = new GameTable();
            Tile tile = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.Up, Direction.UpRight }, AreaType.Castle),
                    BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left, Direction.Down, Direction.DownLeft, Direction.DownRight, Direction.LeftUp, Direction.LeftDown, Direction.RightUp, Direction.RightDown }, AreaType.Field)
                },
                new Position(11, 9))
            { Rotation = TileRotation._180 };
            table.SetTile(tile);

            Assert.IsTrue(table.Areas.Any(area => area.AreaType == AreaType.Castle && area.SubAreas.Count() == 2 && area.IsFinished));
        }
    }
}
