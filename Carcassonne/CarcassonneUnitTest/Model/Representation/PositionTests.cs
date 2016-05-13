using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarcassonneServer.Model.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneServer.Model.Representation.Construction;

namespace CarcassonneServer.Model.Representation.Tests
{
    [TestClass()]
    public class PositionTests
    {
        [TestMethod()]
        public void NeighborDirectionTest()
        {
            Tile t1 = new Tile(new TileDescriptor(
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField),
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField)),
                new Position(0, 0));
            Tile t2 = new Tile(new TileDescriptor(
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField),
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField)),
                new Position(0, 1));

            Assert.AreEqual(Direction.Right, t1.NeighborDirection(t2));
            Assert.AreEqual(Direction.Left, t2.NeighborDirection(t1));
        }

        [TestMethod()]
        public void ConstructionDirectionTest()
        {
            Tile t1 = new Tile(new TileDescriptor(
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField),
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField)),
                new Position(0, 0));

            RoadConstruction r1 = new RoadConstruction(t1);

            Tile t2 = new Tile(new TileDescriptor(
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField),
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField)),
                new Position(0, 1));

            Assert.IsTrue(r1 | t2);
            Assert.AreEqual(Direction.Right, r1.NeighborDirection(t2));
        }
    }
}