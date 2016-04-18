using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using CarcassonneSharedModules.Representation;
using CarcassonneSharedModules.Tools;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class PayloadTest
    {
        [TestMethod]
        public void PlayerWrite()
        {
            var player = new Player(1, "Playa1");

            var one = BitConverter.GetBytes((short)1);
            var newLine = Encoding.Default.GetBytes("\n");
            var playa1 = Encoding.Default.GetBytes("Playa1\n");
            var playa1Guid = Encoding.Default.GetBytes(player.GUID);
            var expected = new byte[one.Length + playa1.Length + playa1Guid.Length + newLine.Length];
            var offset = 0;
            Buffer.BlockCopy(one, 0, expected, 0, one.Length);
            offset += one.Length;
            Buffer.BlockCopy(playa1, 0, expected, offset, playa1.Length);
            offset += playa1.Length;
            Buffer.BlockCopy(playa1Guid, 0, expected, offset, playa1Guid.Length);
            offset += playa1Guid.Length;
            Buffer.BlockCopy(newLine, 0, expected, offset, newLine.Length);
            
            var actual = new byte[short.MaxValue];

            using (var ms = new MemoryStream())
            {
                player.WriteContent(ms);
                actual = ms.ToArray();   
            }
            var actualPlayer = PayloadContentFactory<Player>.Create(actual);

            Assert.AreEqual(expected.Length, actual.Length, "A tömb hossza nem egyezik a várt hosszal.");
            CollectionAssert.AreEqual(expected, actual, "A tömbök nem egyeznek meg.");
            Assert.AreNotSame(player, actualPlayer, "A kapott játékos nem egyezik meg a várttal.");
            Assert.AreEqual<short>(player.Number, actualPlayer.Number, "A kapott játékos száma nem egyezik meg a várttal.");
            Assert.AreEqual<string>(player.Name, actualPlayer.Name, "A kapott játékos neve nem egyezik meg a várttal.");
            Assert.AreEqual<string>(player.GUID, actualPlayer.GUID, "A kapott játékos GUIDja nem egyezik meg a várttal.");
        }

        [TestMethod]
        public void WriteShortTest()
        {
            var val = BitConverter.GetBytes((short)15);
            var min = BitConverter.GetBytes((short)short.MinValue);
            var max = BitConverter.GetBytes((short)short.MaxValue);
            var actualVal = new byte[sizeof(short)];
            var actualMin = new byte[sizeof(short)];
            var actualMax = new byte[sizeof(short)];

            using (var ms = new MemoryStream())
            {
                ms.WriteShort(15);
                actualVal = ms.ToArray();

                ms.Position = 0;
                ms.WriteShort(short.MinValue);
                actualMin = ms.ToArray();

                ms.Position = 0;
                ms.WriteShort(short.MaxValue);
                actualMax = ms.ToArray();
            }


            CollectionAssert.AreEqual(max, actualMax);
            CollectionAssert.AreEqual(min, actualMin);
            CollectionAssert.AreEqual(val, actualVal);
        }
    }
}
