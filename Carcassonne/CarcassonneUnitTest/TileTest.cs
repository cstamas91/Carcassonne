using CarcassonneServer.Model.Representation;
using CarcassonneServer.Model.Representation.Area;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class TileTest
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        
        private Tile GetTileFromDataRow(DataRow row, bool first)
        {
            string subAreaKeyTemplateForDirections = @"Area{0}-{1}";
            string subAreaKeyTemplateForAreaType = @"Area{0}-{1}Type";
            string positionKeyTemplate = @"Poz{0}{1}";
            int tileIndex = first ? 1 : 2;
            //A négy alterület oldalainak beolvasása:
            List<SubArea> subAreas = new List<SubArea>();
            for (int i = 1; i < 5; i++)
            {
                string[] directionStrings = row[string.Format(subAreaKeyTemplateForDirections, i, tileIndex)].ToString().Split(';');
                if (directionStrings.Length > 0 && !string.IsNullOrEmpty(directionStrings[0]))
                {
                    List<Direction> directions = directionStrings.Select(str => (Direction)(int.Parse(str))).ToList();
                    AreaType areaType = (AreaType)int.Parse(row[string.Format(subAreaKeyTemplateForAreaType, i, tileIndex)].ToString());
                    subAreas.Add(new SubArea(directions, areaType));
                }                
            }

            Position p = new Position(
                short.Parse(row[string.Format(positionKeyTemplate, "X", tileIndex)].ToString()),
                short.Parse(row[string.Format(positionKeyTemplate, "Y", tileIndex)].ToString()));

            return new Tile(subAreas, p);
        }

        [TestMethod]
        [DataSource(
            @"Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @"|DataDirectory|\TestData\TestData.csv",
            @"TestData#csv", DataAccessMethod.Sequential)]
        public void TestIsValidAdjacent()
        {
            Tile t1 = GetTileFromDataRow(TestContext.DataRow, true);
            Tile t2 = GetTileFromDataRow(TestContext.DataRow, false);
            bool expected = TestContext.DataRow["Expected"].ToString() == "1" ? true : false;

            bool actual = t1.IsValidAdjacent(t2);

            Assert.AreEqual(expected, actual);
        }
    }
}
