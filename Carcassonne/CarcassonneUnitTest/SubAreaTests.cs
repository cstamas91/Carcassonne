using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class SubAreaTests
    {
        /// <summary>
        /// Should return true.
        /// </summary>
        [TestMethod]
        public void TestCanBeAdjacent()
        {
            Tile t1 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.UpRight, Direction.Up }, AreaType.Castle),
                    BaseSubArea.Get(new List<Direction>() {Direction.RightUp, Direction.LeftUp }, AreaType.Field),
                    BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left }, AreaType.Road),
                    BaseSubArea.Get(new List<Direction>() {Direction.RightDown, Direction.DownRight, Direction.Down, Direction.DownLeft, Direction.LeftDown }, AreaType.Field)
                },
                new Position(10, 9));
            Tile t2 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.Up, Direction.UpRight }, AreaType.Castle),
                    BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left, Direction.Down, Direction.DownLeft, Direction.DownRight, Direction.LeftUp, Direction.LeftDown, Direction.RightUp, Direction.RightDown }, AreaType.Field)
                },
                new Position(11, 9))
            { Rotation = TileRotation._180 };

            Assert.IsTrue(t1[Direction.Up].CanBeAdjacent(t2[Direction.Down]));
        }

        /// <summary>
        /// Should return false.
        /// </summary>
        [TestMethod]
        public void TestCanBeAdjacentNegative()
        {
            Tile t1 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.UpRight, Direction.Up }, AreaType.Castle),
                    BaseSubArea.Get(new List<Direction>() {Direction.RightUp, Direction.LeftUp }, AreaType.Field),
                    BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left }, AreaType.Road),
                    BaseSubArea.Get(new List<Direction>() {Direction.RightDown, Direction.DownRight, Direction.Down, Direction.DownLeft, Direction.LeftDown }, AreaType.Field)
                },
                new Position(10, 9));
            Tile t2 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() {Direction.UpLeft, Direction.Up, Direction.UpRight }, AreaType.Castle),
                    BaseSubArea.Get(new List<Direction>() {Direction.Right, Direction.Left, Direction.Down, Direction.DownLeft, Direction.DownRight, Direction.LeftUp, Direction.LeftDown, Direction.RightUp, Direction.RightDown }, AreaType.Field)
                },
                new Position(11, 9)) { Rotation = TileRotation._180 };

            Assert.IsFalse(t1[Direction.Down].CanBeAdjacent(t2[Direction.Up]));
        }
    }
}
