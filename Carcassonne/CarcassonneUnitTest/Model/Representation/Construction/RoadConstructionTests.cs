using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarcassonneServer.Model.Representation.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation.Construction.Tests
{
    [TestClass()]
    public class RoadConstructionTests
    {
        [TestMethod()]
        public void AddElementTest()
        {
            Tile t1 = new Tile(new TileDescriptor(
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField),
                new TileSideDescriptor(StaticTileSideDescriptor.OpenRoad),
                new TileSideDescriptor(StaticTileSideDescriptor.ClosedField)),
                new Position(0, 0));
            t1.Rotate();
            RoadConstruction r1 = new RoadConstruction(t1, Direction.Left, Direction.Right);

            Assert.AreEqual(TileSideType.Road, t1[Direction.Left].Type);
            Assert.AreEqual(TileSideType.Road, t1[Direction.Right].Type);
            Assert.AreEqual(r1.GUID, t1[Direction.Left].ConstructionGuid);
            Assert.AreEqual(r1.GUID, t1[Direction.Right].ConstructionGuid);

        }
    }
}