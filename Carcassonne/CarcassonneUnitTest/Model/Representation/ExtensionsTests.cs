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
            Assert.AreEqual(ConnectingPoint.Left, ConnectingPoint.Right.Opposite());
            Assert.AreEqual(ConnectingPoint.Right, ConnectingPoint.Left.Opposite());
            Assert.AreEqual(ConnectingPoint.Down, ConnectingPoint.Up.Opposite());
            Assert.AreEqual(ConnectingPoint.Up, ConnectingPoint.Down.Opposite());
        }
    }
}