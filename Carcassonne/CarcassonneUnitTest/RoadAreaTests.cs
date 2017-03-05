using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class RoadAreaTests : BaseTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            //Tile initialTile = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.Up, Direction.Down
            //            }, AreaType.Road),
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.Right, Direction.DownRight, Direction.DownLeft, Direction.UpRight, Direction.RightDown
            //            }, AreaType.Field),
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.Left, Direction.LeftDown, Direction.DownLeft, Direction.LeftUp, Direction.UpLeft
            //            }, AreaType.Field)
            //    }, new Position(0, 0));
            //RoadArea road = BaseArea.Get(initialTile.Areas.FirstOrDefault(sa => sa.AreaType == AreaType.Road)) as RoadArea;

            //Assert.AreEqual(1, road.SubAreas.Count());
            Assert.Fail();
        }
        /// <summary>
        /// Teszteli egy új elem hozzáadását.
        /// </summary>
        [TestMethod]
        public void TestAddTile()
        {
            //Tile initialTile = new Tile(
            //    new List<ISubArea>()
            //    {
            //                    BaseSubArea.Get(
            //                        new List<Direction>()
            //                        {
            //                            Direction.Up, Direction.Down
            //                        }, AreaType.Road),
            //                    BaseSubArea.Get(
            //                        new List<Direction>()
            //                        {
            //                            Direction.Right, Direction.DownRight, Direction.RightUp, Direction.UpRight, Direction.RightDown
            //                        }, AreaType.Field),
            //                    BaseSubArea.Get(
            //                        new List<Direction>()
            //                        {
            //                            Direction.Left, Direction.LeftDown, Direction.DownLeft, Direction.LeftUp, Direction.UpLeft
            //                        }, AreaType.Field)
            //    }, new Position(0, 0));
            //RoadArea road = BaseArea.Get(initialTile.Areas.FirstOrDefault(sa => sa.AreaType == AreaType.Road)) as RoadArea;
            //Tile additionalTile = new Tile(
            //    new List<ISubArea>()
            //    {
            //                    BaseSubArea.Get(
            //                        new List<Direction>()
            //                        {
            //                            Direction.Up, Direction.Down
            //                        }, AreaType.Road),
            //                    BaseSubArea.Get(
            //                        new List<Direction>()
            //                        {
            //                            Direction.Right, Direction.DownRight, Direction.RightUp, Direction.UpRight, Direction.RightDown
            //                        }, AreaType.Field),
            //                    BaseSubArea.Get(
            //                        new List<Direction>()
            //                        {
            //                            Direction.Left, Direction.LeftDown, Direction.DownLeft, Direction.LeftUp, Direction.UpLeft
            //                        }, AreaType.Field)
            //    }, new Position(1, 0));

            //road.AddSubArea(additionalTile.Areas.FirstOrDefault(a => a.AreaType == AreaType.Road));

            //Assert.AreEqual(2, road.SubAreas.Count());
            Assert.Fail();
        }

        /// <summary>
        /// Teszteli a befejezettséget kiértékelő függvényt.
        /// </summary>
        [TestMethod]
        public void TestEvaluateFinished()
        {
            //Tile t1 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
            //            }, AreaType.Field),
            //        BaseSubArea.Get(
            //            new List<Direction>() { Direction.Down }, AreaType.Road)
            //    }, new Position(1, 0));
            //Tile t2 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>() { Direction.Up }, AreaType.Road),
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Down, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
            //            }, AreaType.Field)
            //    }, new Position(0, 0));

            //RoadArea area = BaseArea.Get(t1[Direction.Down]) as RoadArea;
            //area.AddSubArea(t2[Direction.Up]);

            //Assert.IsTrue(area.IsFinished);
            Assert.Fail();
        }

        /// <summary>
        /// Teszteli a befejezettséget kiértékelő függvényt.
        /// </summary>
        [TestMethod]
        public void TestEvaluateFinishedNegative()
        {
            //Tile t1 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
            //            }, AreaType.Field),
            //        BaseSubArea.Get(
            //            new List<Direction>() { Direction.Down }, AreaType.Road)
            //    }, new Position(0, 0));
            //Tile t2 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.Left, Direction.Right, Direction.UpLeft, Direction.UpRight, Direction.DownLeft, Direction.DownRight, Direction.RightDown, Direction.RightUp, Direction.LeftDown, Direction.LeftUp
            //            }, AreaType.Field),
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.Up, Direction.Down
            //            }, AreaType.Road)
            //    }, new Position(-1, 0));
            //RoadArea area = BaseArea.Get(t1[Direction.Down]) as RoadArea;
            //area.AddSubArea(t2[Direction.Up]);

            //Assert.IsFalse(area.IsFinished);
            Assert.Fail();
        }

        [TestMethod]
        public void TestMerge()
        {
            //Tile t1 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
            //            }, AreaType.Field),
            //        BaseSubArea.Get(
            //            new List<Direction>() { Direction.Down }, AreaType.Road)
            //    }, new Position(1, 0));
            //RoadArea r1 = BaseArea.Get(t1[Direction.Down]) as RoadArea;

            //Tile t2 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(
            //            new List<Direction>() { Direction.Up }, AreaType.Road),
            //        BaseSubArea.Get(
            //            new List<Direction>()
            //            {
            //                Direction.DownLeft, Direction.LeftDown, Direction.Left, Direction.LeftUp, Direction.UpLeft, Direction.Down, Direction.UpRight, Direction.RightUp, Direction.Right, Direction.RightDown, Direction.DownRight
            //            }, AreaType.Field)
            //    }, new Position(0, 0));
            //RoadArea r2 = BaseArea.Get(t2[Direction.Up]) as RoadArea;

            //var merged = r1.Merge(r2);

            //Assert.AreEqual(2, merged.SubAreas.Count());
            //Assert.IsTrue(merged.IsFinished);
            Assert.Fail();
        }

        [TestMethod]
        public void TestScore()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestLoop()
        {
            //var roadEdges = new List<Direction>() { Direction.Left, Direction.Down };
            //var smallFieldEdges = new List<Direction>() { Direction.LeftDown, Direction.DownLeft };

            //Tile t1 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(roadEdges, AreaType.Road),
            //        BaseSubArea.Get(smallFieldEdges, AreaType.Field),
            //        BaseSubArea.Get(Extensions.GetEnumValues<Direction>().Except(roadEdges).Except(smallFieldEdges).ToList(), AreaType.Field )
            //    }, new Position(11, 9));
            //Tile t2 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(roadEdges, AreaType.Road),
            //        BaseSubArea.Get(smallFieldEdges, AreaType.Field),
            //        BaseSubArea.Get(Extensions.GetEnumValues<Direction>().Except(roadEdges).Except(smallFieldEdges).ToList(), AreaType.Field )
            //    }, new Position(11, 8)) { Rotation = TileRotation._90 };
            //Tile t3 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(roadEdges, AreaType.Road),
            //        BaseSubArea.Get(smallFieldEdges, AreaType.Field),
            //        BaseSubArea.Get(Extensions.GetEnumValues<Direction>().Except(roadEdges).Except(smallFieldEdges).ToList(), AreaType.Field )
            //    }, new Position(10, 8)) { Rotation = TileRotation._180 };
            //Tile t4 = new Tile(
            //    new List<ISubArea>()
            //    {
            //        BaseSubArea.Get(new List<Direction>() { Direction.Left }, AreaType.Road),
            //        BaseSubArea.Get(new List<Direction>() { Direction.Down }, AreaType.Road),
            //        BaseSubArea.Get(new List<Direction>() { Direction.Right }, AreaType.Road),
            //        BaseSubArea.Get(new List<Direction>() { Direction.LeftDown, Direction.DownLeft }, AreaType.Field),
            //        BaseSubArea.Get(new List<Direction>() { Direction.RightDown, Direction.DownRight }, AreaType.Field),
            //        BaseSubArea.Get(new List<Direction>() { Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.RightUp }, AreaType.Field)
            //    }, new Position(10, 9)) { Rotation = TileRotation._270 };
            //RoadArea roadArea = BaseArea.Get(t1[Direction.Down]) as RoadArea;
            //roadArea.AddSubArea(t2[Direction.Right]);
            //roadArea.AddSubArea(t3[Direction.Up]);
            //roadArea.AddSubArea(t4[Direction.Left]);
            //roadArea.AddSubArea(t4[Direction.Up]);

            //Assert.IsTrue(roadArea.IsFinished);
            Assert.Fail();
        }
    }
}
