using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarcassonneServer.Model.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void OppositeTest()
        {
            Assert.AreEqual(Direction.Left, Direction.Right.Opposite());
            Assert.AreEqual(Direction.Right, Direction.Left.Opposite());
            Assert.AreEqual(Direction.Down, Direction.Up.Opposite());
            Assert.AreEqual(Direction.Up, Direction.Down.Opposite());
        }
    }
}