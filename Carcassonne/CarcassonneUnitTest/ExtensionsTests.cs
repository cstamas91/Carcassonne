using CarcassonneUnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.Tests
{
    [TestClass()]
    public class ExtensionsTests : BaseTest
    {
        [TestMethod()]
        [DataSource(
            providerInvariantName,
            @"|DataDirectory|\TestData\TestOpposite.csv",
            @"TestOpposite#csv", DataAccessMethod.Sequential)]
        public void TestOpposite()
        {
            Direction baseValue = ReadEnum<Direction>(TestContext.DataRow, "Direction");
            Direction expected = ReadEnum<Direction>(TestContext.DataRow, "Expected");
            Direction actual = baseValue.Opposite();
            Assert.AreEqual(expected, actual, string.Format("{0} is not opposite to {1}", expected.ToString(), actual.ToString()));
        }

        [TestMethod]
        public void TestRotationAdjustedDirection()
        {
            var directionValues = new List<short>()
            {
                0,1,2,3,4,5,6,7,8,9,10,11
            };

            foreach (TileRotation rotation in Enum.GetValues(typeof(TileRotation)))
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if (!directionValues.Contains((short)direction))
                        Assert.Fail(string.Format("{0}", direction));
                }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestDown180()
        {
            Direction down = Direction.Down;
            TileRotation _180 = TileRotation._180;

            Assert.AreEqual(Direction.Up, down.RotationAdjustedDirection(_180));
        }
    }
}