using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class TileTest : BaseTest
    {
        private const string subAreaKeyTemplateForDirections = @"Area{0}-{1}"; // 0: subareaIndex, 1: tileIndex
        private const string subAreaKeyTemplateForAreaType = @"Area{0}-{1}Type"; // 0: subareaIndex, 1: tileIndex
        private const string positionKeyTemplate = @"Poz{0}{1}"; // 0: tengely, 1: tileIndex

        private Tuple<Tile, Tile> ReadTestIsValidAdjacentData(DataRow row)
        {
            Tile[] tiles = new Tile[2];
            //A négy alterület oldalainak beolvasása:
            for (int tileIndex = 1; tileIndex < 3; tileIndex++)
            {
                List<SubArea> subAreas = new List<SubArea>();
                for (int subareIndex = 1; subareIndex < 5; subareIndex++)
                {
                    List<Direction> directions = ReadEnumList<Direction>(row, string.Format(subAreaKeyTemplateForDirections, subareIndex, tileIndex));

                    if (directions.Count == 0)
                        continue;

                    AreaType areaType = ReadEnum<AreaType>(row, string.Format(subAreaKeyTemplateForAreaType, subareIndex, tileIndex));
                    subAreas.Add(new SubArea(directions, areaType));
                }

                Position p = new Position(
                short.Parse(row[string.Format(positionKeyTemplate, "X", tileIndex)].ToString()),
                short.Parse(row[string.Format(positionKeyTemplate, "Y", tileIndex)].ToString()));

                tiles[tileIndex - 1] = new Tile(subAreas, p);
            }
            return new Tuple<Tile, Tile>(tiles[0], tiles[1]);
        }
        private Tile ReadTestRotateData(DataRow row)
        {
            TileRotation rotation = ReadEnum<TileRotation>(row, "Rotation");
            Tile t = new Tile();
            t.Rotation = rotation;
            return t;
        }

        [TestMethod]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestRotate.csv",
            @"TestRotate#csv", DataAccessMethod.Sequential)]
        public void TestRotate()
        {
            Tile tile = ReadTestRotateData(TestContext.DataRow);
            TileRotation expected = ReadEnum<TileRotation>(TestContext.DataRow, "Expected");

            tile.Rotate();

            Assert.AreEqual(expected, tile.Rotation);
        }

        [TestMethod]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestIsValidAdjacent.csv",
            @"TestIsValidAdjacent#csv", DataAccessMethod.Sequential)]
        public void TestIsValidAdjacent()
        {
            Tuple<Tile, Tile> tiles = ReadTestIsValidAdjacentData(TestContext.DataRow);
            bool expected = TestContext.DataRow["Expected"].ToString() == "1" ? true : false;
            bool actual = tiles.Item1.IsValidAdjacent(tiles.Item2);
            Assert.AreEqual(expected, actual);
        }
    }
}
