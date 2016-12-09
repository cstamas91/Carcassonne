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
        private const string areaDescriptionKeyTemplate = @"AreaDescription{0}"; //0: tileIndex
        private const string positionKeyTemplate = @"Poz{0}{1}"; // 0: tengely, 1: tileIndex

        private Tile ReadTile(DataRow row, int index)
        {
            Position position = new Position(
                Convert.ToInt16(row[string.Format(positionKeyTemplate, "X", index)].ToString()),
                Convert.ToInt16(row[string.Format(positionKeyTemplate, "Y", index)].ToString()));

            var areaData = row[string.Format(areaDescriptionKeyTemplate, index)].ToString().Split(';');
            List<SubArea> subAreas = new List<SubArea>();
            foreach (string subArea in areaData)
            {
                var subAreaData = subArea.Split(':');
                AreaType subAreaType = (AreaType)Enum.Parse(typeof(AreaType), subAreaData[0]);

                List<Direction> directions = subAreaData[1].Split('-').Select(str => (Direction)Enum.Parse(typeof(Direction), str)).ToList();
                subAreas.Add(new SubArea(directions, subAreaType));
            }

            return new Tile(subAreas, position);
        }

        private Tuple<Tile, Tile> ReadTestIsValidAdjacentData(DataRow row)
        {
            Tile t1 = ReadTile(row, 1);
            Tile t2 = ReadTile(row, 2);
            return new Tuple<Tile, Tile>(t1, t2);
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
