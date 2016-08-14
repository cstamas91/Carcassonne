using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarcassonneServer.Model.Representation.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation.Construction.Tests
{
    [TestClass()]
    public class RoadConstructionTests
    {
        [TestMethod()]
        public void AddElementTest()
        {
            Tile t = new Tile(
                    new TileDescriptor(
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedRoad),
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedField),
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedField),
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedField)),
                    new Position(0, 0));
            RoadConstruction r1 = new RoadConstruction(ref t);

            Tile tileToAdd = new Tile(
                    new TileDescriptor(
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedField),
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedField),
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedRoad),
                        TileSideDescriptorFactory.Factory(StaticTileSideDescriptor.ClosedField)),
                    new Position(1, 0));

            try
            {
                r1.AddElement(ref tileToAdd);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is TileMatchException)
            {
                Console.WriteLine("Oops.");
            }

            Assert.AreEqual(true, r1.IsFinished);
            Assert.AreEqual(2, r1.Size);
        }
    }
}