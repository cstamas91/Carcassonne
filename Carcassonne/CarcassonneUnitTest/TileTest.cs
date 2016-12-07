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
    public class TileTest
    {
        private TestContext testContextInstance;
        private const string providerInvariantName = @"Microsoft.VisualStudio.TestTools.DataSource.CSV";
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public TileTest()
        {
            Debug.WriteLine("|DataDirectory|");
        }

        private Tuple<Tile, Tile> TestIsValidAdjacentDataReader(DataRow row)
        {
            Tile[] tiles = new Tile[2];
            string subAreaKeyTemplateForDirections = @"Area{0}-{1}"; // 0: subareaIndex, 1: tileIndex
            string subAreaKeyTemplateForAreaType = @"Area{0}-{1}Type"; // 0: subareaIndex, 1: tileIndex
            string positionKeyTemplate = @"Poz{0}{1}"; // 0: tengely, 1: tileIndex
            //A négy alterület oldalainak beolvasása:
            for (int tileIndex = 1; tileIndex < 3; tileIndex++)
            {
                List<SubArea> subAreas = new List<SubArea>();
                for (int subareIndex = 1; subareIndex < 5; subareIndex++)
                {
                    string[] directionStrings = row[string.Format(subAreaKeyTemplateForDirections, subareIndex, tileIndex)].ToString().Split(';');
                    if (directionStrings.Length > 0 && !string.IsNullOrEmpty(directionStrings[0]))
                    {
                        List<Direction> directions = directionStrings.Select(str => (Direction)(int.Parse(str))).ToList();
                        AreaType areaType = (AreaType)int.Parse(row[string.Format(subAreaKeyTemplateForAreaType, subareIndex, tileIndex)].ToString());
                        subAreas.Add(new SubArea(directions, areaType));
                    }
                }
                Position p = new Position(
                short.Parse(row[string.Format(positionKeyTemplate, "X", tileIndex)].ToString()),
                short.Parse(row[string.Format(positionKeyTemplate, "Y", tileIndex)].ToString()));

                tiles[tileIndex - 1] = new Tile(subAreas, p);
            }
            return new Tuple<Tile, Tile>(tiles[0], tiles[1]);
        }

        private Tile TestRotateDataReader(DataRow row)
        {
            throw new NotImplementedException("testRotateDataReader");
        }


        [TestMethod]
        [DataSource(
            providerInvariantName,
            //@"D:\Git\Carcassonne\Carcassonne\CarcassonneUnitTest\TestData\TestRotate\TestRotate.csv",
            @"|DataDirectory|\TestData\TestRotate.csv",
            @"TestRotate#csv", DataAccessMethod.Sequential)]
        public void TestRotate()
        {
            Assert.Fail("Implementációt befejezni.");
        }

        [TestMethod]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestIsValidAdjacent.csv",
            @"TestIsValidAdjacent#csv", DataAccessMethod.Sequential)]
        public void TestIsValidAdjacent()
        {
            Tuple<Tile, Tile> tiles = TestIsValidAdjacentDataReader(TestContext.DataRow);
            bool expected = TestContext.DataRow["Expected"].ToString() == "1" ? true : false;
            bool actual = tiles.Item1.IsValidAdjacent(tiles.Item2);
            Assert.AreEqual(expected, actual);
        }

        
    }
}
