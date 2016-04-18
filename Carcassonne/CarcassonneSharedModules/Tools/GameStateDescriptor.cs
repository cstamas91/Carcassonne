using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Representation;
using System.IO;

namespace CarcassonneSharedModules.Tools
{
    public class GameStateDescriptor : IPayloadContent
    {
        public GameTable Table { get; private set; }
        public ScoreHandler Scores { get; private set; }

        public GameStateDescriptor() { }

        private GameStateDescriptor(GameTable table, ScoreHandler scores)
        {
            Table = table;
            Scores = scores;
        }

        public static GameStateDescriptor GameStateDescriptorFactory(GameTable table, ScoreHandler scores)
        {
            return new GameStateDescriptor(table, scores);
        }

        public static GameStateDescriptor GameStateDescriptorFactory(IGameModel model)
        {
            return GameStateDescriptorFactory(model.GameTable, model.ScoreHandler);
        }

        #region IPayloadContent
        public void ReadContent(byte[] payloadContent)
        {
            using (var ms = new MemoryStream(payloadContent))
            {
                var countContent = new byte[sizeof(short)];
                ms.Read(countContent, 0, sizeof(short));

                var playerCount = BitConverter.ToInt16(countContent, 0);
                var scoreContent = new byte[sizeof(short) * 4];
                ms.Read(scoreContent, 0, sizeof(short) * playerCount);
                Scores = PayloadContentFactory<ScoreHandler>.Create(scoreContent);

                /* mivel a játéktábla reprezentált mérete nem tudható előre, az üzenet végére rakjuk, így ha végeztünk a pontok olvasásával, onnan csak a táblát olvassuk
                 * a stream maradékát belemásoljuk egy másik streambe, amit byte arrayként átadunk a GameTable factoryjának */
                using (var tableStream = new MemoryStream())
                {
                    ms.CopyTo(tableStream);
                    Table = PayloadContentFactory<GameTable>.Create(tableStream.ToArray());
                }   
            }
        }

        public void WriteContent(Stream contentStream)
        {
            Scores.WriteContent(contentStream);
            Table.WriteContent(contentStream);
        }
        #endregion IPayloadContent
    }
}
