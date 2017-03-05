using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.GameItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class TileTests : BaseTest
    {
        /// <summary>
        /// Teszteli, hogy egy mező elforgatása után a várt alterületeket érjük el az irány indexerrel.
        /// </summary>
        [TestMethod]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestRotate.csv",
            @"TestRotate#csv", DataAccessMethod.Sequential)]
        public void TestRotate()
        {
            Tile tile = tiles[TileTypeEnum.Crossroads]();
            tile.Rotation = (TileRotation)TestContext.DataRow["Rotation"];
            TileRotation expected = (TileRotation)TestContext.DataRow["Expected"];

            tile.Rotate();

            Assert.AreEqual(expected, tile.Rotation);
        }
        /// <summary>
        /// Teszteli az egymás mellé elhelyezhetőséget kiértékelő függvényt.
        /// </summary>
        [TestMethod]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestIsValidAdjacent.csv",
            @"TestIsValidAdjacent#csv", DataAccessMethod.Sequential)]
        public void TestIsValidAdjacent()
        {
            Tile t1 = tiles[(TileTypeEnum)TestContext.DataRow["Type1"]]();
            Tile t2 = tiles[(TileTypeEnum)TestContext.DataRow["Type2"]]();
            t1.Rotation = (TileRotation)TestContext.DataRow["Rotation1"];
            t2.Rotation = (TileRotation)TestContext.DataRow["Rotation2"];
            t1.X = (int)TestContext.DataRow["P1X"];
            t1.Y = (int)TestContext.DataRow["P1Y"];
            t2.X = (int)TestContext.DataRow["P2X"];
            t2.Y = (int)TestContext.DataRow["P2Y"];

            Assert.AreEqual(bool.Parse((string)TestContext.DataRow["Expected"]), t1.IsValidAdjacent(t2));

        }
        /// <summary>
        /// Teszteli az irány indexert.
        /// </summary>
        [TestMethod]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestIndexer.csv",
            @"TestIndexer#csv", DataAccessMethod.Sequential)]
        public void TestIndexer()
        {
            TileTypeEnum tileType = (TileTypeEnum)TestContext.DataRow["Type"];
            TileRotation tileRotation = (TileRotation)TestContext.DataRow["Rotation"];
            Direction direction = (Direction)TestContext.DataRow["Direction"];

            Tile tile = tiles[tileType]();
            tile.Rotation = tileRotation;

            AreaType expected = (AreaType)TestContext.DataRow["Expected"];
            Assert.AreEqual(expected, tile[direction].AreaType);
        }

        private Dictionary<TileTypeEnum, Func<Tile>> tiles = new Dictionary<TileTypeEnum, Func<Tile>>()
        {
            { TileTypeEnum.Crossroads, Activator.CreateInstance<CrossRoads> }
        };

        enum TileTypeEnum
        {
            Crossroads = 18,
        }
    }
}
