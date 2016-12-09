using CarcassonneUnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}