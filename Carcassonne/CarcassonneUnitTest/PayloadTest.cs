using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carcassonne.Model.Representation;
using Carcassonne.Model.Tools;
using System.Text;
using System.IO;

namespace CarcassonneUnitTest
{
    [TestClass]
    public class PayloadTest
    {
        [TestMethod]
        public void PlayerWrite()
        {
            var one = BitConverter.GetBytes((short)1);
            var playa1 = Encoding.Default.GetBytes("Playa1\n");
            var expected = new byte[one.Length + playa1.Length];
            Buffer.BlockCopy(one, 0, expected, 0, one.Length);
            Buffer.BlockCopy(playa1, 0, expected, one.Length, playa1.Length);
            var player = new Player(1, "Playa1");
            var actual = new byte[short.MaxValue];

            using (var ms = new MemoryStream())
            {
                player.WriteContent(ms);
                actual = ms.ToArray();   
            }
            var actualPlayer = PayloadContentFactory<Player>.Create(actual);
            
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreNotSame(player, actualPlayer);
            Assert.AreEqual<short>(player.Number, actualPlayer.Number);
            Assert.AreEqual<string>(player.Name, actualPlayer.Name);
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
