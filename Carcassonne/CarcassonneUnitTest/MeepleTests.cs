using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using CarcassonneServer.Model.Representation.SubAreas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class MeepleTests : BaseTest
    {
        Player player1;

        public MeepleTests()
        {
            player1 = new Player(1, "TestPlayer1");
        }

        [TestMethod]
        public void SetMeepleOnSubArea()
        {
            Tile tile = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() { Direction.Up, Direction.Down }, AreaType.Road),
                    BaseSubArea.Get(new List<Direction>() { Direction.UpRight, Direction.DownRight, Direction.Right, Direction.RightUp, Direction.RightDown }, AreaType.Field),
                    BaseSubArea.Get(new List<Direction>() { Direction.UpLeft, Direction.DownLeft, Direction.Left, Direction.LeftDown, Direction.LeftUp }, AreaType.Road),
                }, new Position(11, 9));
            Meeple meeple = new Meeple(player1);
            tile[Direction.Up].SetMeeple(meeple);
            
            Assert.IsNotNull(tile[Direction.Up].Meeple);
        }

        [TestMethod]
        public void SetMeepleOnArea()
        {
            Tile tile = new Tile(
                new List<ISubArea>()
                {
                    BaseSubArea.Get(new List<Direction>() { Direction.Up, Direction.Down }, AreaType.Road),
                    BaseSubArea.Get(new List<Direction>() { Direction.UpRight, Direction.DownRight, Direction.Right, Direction.RightUp, Direction.RightDown }, AreaType.Field),
                    BaseSubArea.Get(new List<Direction>() { Direction.UpLeft, Direction.DownLeft, Direction.Left, Direction.LeftDown, Direction.LeftUp }, AreaType.Road),
                }, new Position(11, 9));
            var roadArea = BaseArea.Get(tile[Direction.Up]);
            Meeple meeple = new Meeple(player1);

            roadArea.AddMeeple(meeple, tile[Direction.Up].Id);

            Assert.IsTrue(roadArea.Owners.Contains(player1));
        }
    }
}
