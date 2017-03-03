using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class RoadAreaTest : BaseTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Tile initialTile = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Up, Direction.Down
                        }, AreaType.Road),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Right, Direction.DownRight, Direction.DownLeft, Direction.UpRight, Direction.RightDown
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Left, Direction.LeftDown, Direction.DownLeft, Direction.LeftUp, Direction.UpLeft
                        }, AreaType.Field)
                }, new Position(0, 0));
            RoadArea road = BaseArea.Get(initialTile.Areas.FirstOrDefault(sa => sa.AreaType == AreaType.Road)) as RoadArea;

            Assert.AreEqual(1, road.SubAreas.Count());
        }
        /// <summary>
        /// Teszteli egy új elem hozzáadását.
        /// </summary>
        [TestMethod]
        public void TestAddTile()
        {
            Tile initialTile = new Tile(
                new List<ISubArea>()
                {
                                BaseSubArea.Get(
                                    new List<Direction>()
                                    {
                                        Direction.Up, Direction.Down
                                    }, AreaType.Road),
                                BaseSubArea.Get(
                                    new List<Direction>()
                                    {
                                        Direction.Right, Direction.DownRight, Direction.RightUp, Direction.UpRight, Direction.RightDown
                                    }, AreaType.Field),
                                BaseSubArea.Get(
                                    new List<Direction>()
                                    {
                                        Direction.Left, Direction.LeftDown, Direction.DownLeft, Direction.LeftUp, Direction.UpLeft
                                    }, AreaType.Field)
                }, new Position(0, 0));
            RoadArea road = BaseArea.Get(initialTile.Areas.FirstOrDefault(sa => sa.AreaType == AreaType.Road)) as RoadArea;
            Tile additionalTile = new Tile(
                new List<ISubArea>()
                {
                                BaseSubArea.Get(
                                    new List<Direction>()
                                    {
                                        Direction.Up, Direction.Down
                                    }, AreaType.Road),
                                BaseSubArea.Get(
                                    new List<Direction>()
                                    {
                                        Direction.Right, Direction.DownRight, Direction.RightUp, Direction.UpRight, Direction.RightDown
                                    }, AreaType.Field),
                                BaseSubArea.Get(
                                    new List<Direction>()
                                    {
                                        Direction.Left, Direction.LeftDown, Direction.DownLeft, Direction.LeftUp, Direction.UpLeft
                                    }, AreaType.Field)
                }, new Position(1, 0));

            road.AddSubArea(additionalTile.Areas.FirstOrDefault(a => a.AreaType == AreaType.Road));

            Assert.AreEqual(2, road.SubAreas.Count());
        }

        /// <summary>
        /// Teszteli egy ember hozzáadását.
        /// </summary>
        [TestMethod]
        public void TestAddMeeple()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Teszteli a befejezettséget kiértékelő függvényt.
        /// </summary>
        [TestMethod]
        public void TestEvaluateFinished()
        {
            Tile t1 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>() { Direction.Down }, AreaType.Road)
                }, new Position(1, 0));
            Tile t2 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>() { Direction.Up }, AreaType.Road),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Down, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
                        }, AreaType.Field)
                }, new Position(0, 0));

            RoadArea area = BaseArea.Get(t1[Direction.Down]) as RoadArea;
            area.AddSubArea(t2[Direction.Up]);

            Assert.IsTrue(area.IsFinished);
        }

        /// <summary>
        /// Teszteli a befejezettséget kiértékelő függvényt.
        /// </summary>
        [TestMethod]
        public void TestEvaluateFinishedNegative()
        {
            Tile t1 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>() { Direction.Down }, AreaType.Road)
                }, new Position(0, 0));
            Tile t2 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Left, Direction.Right, Direction.UpLeft, Direction.UpRight, Direction.DownLeft, Direction.DownRight, Direction.RightDown, Direction.RightUp, Direction.LeftDown, Direction.LeftUp
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.Up, Direction.Down
                        }, AreaType.Road)
                }, new Position(-1, 0));
            RoadArea area = BaseArea.Get(t1[Direction.Down]) as RoadArea;
            area.AddSubArea(t2[Direction.Up]);

            Assert.IsFalse(area.IsFinished);
        }

        [TestMethod]
        public void TestMerge()
        {
            Tile t1 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
                        }, AreaType.Field),
                    BaseSubArea.Get(
                        new List<Direction>() { Direction.Down }, AreaType.Road)
                }, new Position(1, 0));
            RoadArea r1 = BaseArea.Get(t1[Direction.Down]) as RoadArea;

            Tile t2 = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(
                        new List<Direction>() { Direction.Up }, AreaType.Road),
                    BaseSubArea.Get(
                        new List<Direction>()
                        {
                            Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Down, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
                        }, AreaType.Field)
                }, new Position(0, 0));
            RoadArea r2 = BaseArea.Get(t2[Direction.Up]) as RoadArea;

            var merged = r1.Merge(r2);

            Assert.AreEqual(2, merged.SubAreas.Count());
            Assert.IsTrue(merged.IsFinished);
        }

        [TestMethod]
        public void TestScore()
        {
            Assert.Fail();
        }
    }
}
