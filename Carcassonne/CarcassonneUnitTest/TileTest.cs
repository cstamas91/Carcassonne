using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Construction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void RotateTileTest()
        {
            Tile tile1 = new Tile(TileDescriptor.CurvyRoad);  // up,right = road; down,left = field;
            tile1.Rotate(); // left, up = field; right, down = road;

            Assert.AreEqual(TileSideType.Field, tile1.Left.Type);
            Assert.AreEqual(TileSideType.Field, tile1.Up.Type);
            Assert.AreEqual(TileSideType.Road, tile1.Right.Type);
            Assert.AreEqual(TileSideType.Road, tile1.Down.Type);
        }

        [TestMethod]
        public void NeighbourTest()
        {
            Tile road1 = new Tile(TileDescriptor.CurvyRoad);
            Tile road2 = new Tile(TileDescriptor.CurvyRoad);
            road1.Position = new Position(0, 0);
            road2.Position = new Position(1, 0);
            RoadConstruction construction = new RoadConstruction(road1);

            Assert.IsTrue(road1 | road2);
            Assert.IsTrue(construction | road2);          
        }
    }
}
