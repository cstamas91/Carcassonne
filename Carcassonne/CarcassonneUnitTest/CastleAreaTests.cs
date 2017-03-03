using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class CastleAreaTests
    {
        [TestMethod]
        public void TestEvaluateFinished()
        {
            Tile t1 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Up, Direction.UpLeft, Direction.UpRight,
                            Direction.Left, Direction.LeftUp, Direction.LeftDown,
                            Direction.Right, Direction.RightUp, Direction.RightDown
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Down, Direction.DownLeft, Direction.DownRight
                        }, AreaType.Castle)
                }, new Position(1, 0));
            Tile t2 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Up, Direction.UpLeft, Direction.UpRight,
                            Direction.Left, Direction.LeftUp, Direction.LeftDown,
                            Direction.Right, Direction.RightUp, Direction.RightDown
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Down, Direction.DownLeft, Direction.DownRight
                        }, AreaType.Castle)
                }, new Position(0, 0));
            t2.Rotation = TileRotation._180;
            CastleArea area = BaseArea.Get(t1[Direction.Down]) as CastleArea;
            area.AddSubArea(t2[Direction.Up]);

            Assert.IsTrue(area.IsFinished);            
        }
    }
}
