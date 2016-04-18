using CarcassonneSharedModules.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CarcassonneSharedModules.Tools
{
    /// <summary>
    /// A játék inicializáláshoz szükséges információkat tartalmazó osztály. A kliensek a játék kezdete előtt megkapják.
    /// </summary>
    public class InitContent : IPayloadContent
    {
        public IEnumerable<Player> Players { get; private set; }

        public InitContent() { }
        public InitContent(IEnumerable<Player> players)
        {
            Players = players;
        }

        #region IPayloadContent

        public void ReadContent(byte[] payloadContent)
        {
            var ls = new List<Player>();
            using (var ms = new MemoryStream(payloadContent))
            {
                var count = new byte[sizeof(short)];
                ms.Read(count, 0, sizeof(short));

                for (int i = 0; i < BitConverter.ToInt16(count, 0); i++)
                {
                    ls.Add(PayloadContentFactory<Player>.Create(ms.ReadToChar('\n', 2)));

                    #region for reference
                    //using (var playerStream = new MemoryStream()) //stream for playerdata
                    //{
                    //    var buff = new byte[2];
                    //    ms.Read(buff, 0, 1);
                    //    var c = BitConverter.ToChar(buff, 0);
                    //    while (c != '\n')
                    //    {
                    //        playerStream.Write(buff, 0, 1);
                    //        ms.Read(buff, 0, 1);
                    //        c = BitConverter.ToChar(buff, 0);
                    //    }
                        
                    //    var arr = playerStream.ToArray();
                    //    ls.Add(PayloadContentFactory<Player>.Create(arr));
                    //}
                    #endregion for reference
                }
            }
            Players = ls;
        }

        public void WriteContent(Stream contentStream)
        {
            var playerList = new List<Player>(Players);

            contentStream.WriteShort((short)playerList.Count);
            foreach (var player in playerList)
                player.WriteContent(contentStream);
        }

        #endregion IPayloadContent
    }
}
