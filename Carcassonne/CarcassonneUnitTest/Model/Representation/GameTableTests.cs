using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarcassonneServer.Model.Representation.Tests
{
    [TestClass()]
    public class GameTableTests
    {
        [TestMethod()]
        public void SetTileTest()
        {
            var table = new GameTable();
            Tile t1 = new Tile(TileDescriptor.Monastery, new Position(0, 0));
            table.SetTile(t1);

            Assert.IsTrue(table.Constructions.Count() == 2);
        }
    }
}